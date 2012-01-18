﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Xml.Serialization;
using System.Xml;
using Bonsai.Expressions;
using Bonsai.Dag;
using Bonsai.Design;
using System.Reactive.Disposables;
using System.Linq.Expressions;
using Bonsai.Editor.Properties;

namespace Bonsai.Editor
{
    public partial class MainForm : Form
    {
        const int CtrlModifier = 0x8;

        EditorSite editorSite;
        WorkflowBuilder workflowBuilder;
        XmlSerializer serializer;
        Dictionary<Type, Type> typeVisualizers;
        ExpressionBuilderGraph runningWorkflow;
        Dictionary<GraphNode, Action<bool>> visualizerMapping;
        ExpressionBuilderTypeConverter builderConverter;

        IDisposable loaded;
        IDisposable running;

        public MainForm()
        {
            InitializeComponent();
            InitializeToolbox();

            editorSite = new EditorSite(this);
            workflowBuilder = new WorkflowBuilder();
            serializer = new XmlSerializer(typeof(WorkflowBuilder));
            typeVisualizers = TypeVisualizerLoader.GetTypeVisualizerDictionary();
            builderConverter = new ExpressionBuilderTypeConverter();
            propertyGrid.Site = editorSite;
        }

        #region Toolbox

        void InitializeToolbox()
        {
            var packages = WorkflowElementLoader.GetWorkflowElementTypes();
            foreach (var package in packages)
            {
                InitializeToolboxCategory(package.Key, package);
            }

            InitializeToolboxCategory("Combinator", new[]
            {
                typeof(TimestampBuilder), typeof(SkipUntilBuilder), typeof(TakeUntilBuilder),
                typeof(SampleBuilder), typeof(SampleIntervalBuilder), typeof(CombineLatestBuilder),
                typeof(ConcatBuilder), typeof(ZipBuilder), typeof(AmbBuilder)
            });
        }

        string GetPackageDisplayName(string packageKey)
        {
            return packageKey.Replace("Bonsai.", string.Empty);
        }

        void InitializeToolboxCategory(string categoryName, IEnumerable<Type> types)
        {
            var category = toolboxTreeView.Nodes.Add(categoryName, GetPackageDisplayName(categoryName));

            foreach (var type in types)
            {
                var name = type.IsSubclassOf(typeof(ExpressionBuilder)) ? type.Name.Remove(type.Name.LastIndexOf("Builder")) : type.Name;
                var elementType = LoadableElementType.FromType(type);
                var elementTypeNode = elementType == null ? category : category.Nodes[elementType.ToString()];
                if (elementTypeNode == null)
                {
                    elementTypeNode = category.Nodes.Add(elementType.ToString(), elementType.ToString());
                }

                elementTypeNode.Nodes.Add(type.AssemblyQualifiedName, name);
            }
        }

        #endregion

        #region File Menu

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workflowBuilder.Workflow.Clear();
            commandExecutor.Clear();
            UpdateGraphLayout();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openWorkflowDialog.ShowDialog() == DialogResult.OK)
            {
                saveWorkflowDialog.FileName = openWorkflowDialog.FileName;
                using (var reader = XmlReader.Create(openWorkflowDialog.FileName))
                {
                    workflowBuilder = (WorkflowBuilder)serializer.Deserialize(reader);
                    commandExecutor.Clear();
                    UpdateGraphLayout();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saveWorkflowDialog.FileName)) saveAsToolStripMenuItem_Click(this, e);
            else
            {
                using (var writer = XmlWriter.Create(saveWorkflowDialog.FileName, new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(writer, workflowBuilder);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveWorkflowDialog.ShowDialog() == DialogResult.OK)
            {
                saveToolStripMenuItem_Click(this, e);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            StopWorkflow();
            base.OnClosing(e);
        }

        #endregion

        #region Workflow Model

        void ConnectGraphNodes(GraphNode graphViewSource, GraphNode graphViewTarget)
        {
            var source = (Node<ExpressionBuilder, ExpressionBuilderParameter>)graphViewSource.Tag;
            var target = (Node<ExpressionBuilder, ExpressionBuilderParameter>)graphViewTarget.Tag;
            if (workflowBuilder.Workflow.Successors(source).Contains(target)) return;
            var connection = string.Empty;

            var combinator = target.Value as CombinatorBuilder;
            if (combinator != null)
            {
                if (!workflowBuilder.Workflow.Predecessors(target).Any()) connection = "Source";
                else
                {
                    var binaryCombinator = combinator as BinaryCombinatorBuilder;
                    if (binaryCombinator != null && binaryCombinator.Other == null)
                    {
                        connection = "Other";
                    }
                }
            }

            if (!string.IsNullOrEmpty(connection))
            {
                var parameter = new ExpressionBuilderParameter(connection);
                commandExecutor.Execute(
                () =>
                {
                    workflowBuilder.Workflow.AddEdge(source, target, parameter);
                    UpdateGraphLayout();
                },
                () =>
                {
                    workflowBuilder.Workflow.RemoveEdge(source, target, parameter);
                    UpdateGraphLayout();
                });
            }
        }

        void CreateGraphNode(string typeName, GraphNode closestGraphViewNode, bool branch)
        {
            var type = Type.GetType(typeName);
            if (running == null && type != null)
            {
                ExpressionBuilder builder;
                var elementType = LoadableElementType.FromType(type);
                if (type.IsSubclassOf(typeof(LoadableElement)))
                {
                    var element = (LoadableElement)Activator.CreateInstance(type);
                    builder = ExpressionBuilder.FromLoadableElement(element);
                }
                else builder = (ExpressionBuilder)Activator.CreateInstance(type);

                var node = new Node<ExpressionBuilder, ExpressionBuilderParameter>(builder);
                Action addNode = () => workflowBuilder.Workflow.Add(node);
                Action removeNode = () => workflowBuilder.Workflow.Remove(node);
                Action addConnection = () => { };
                Action removeConnection = () => { };

                var closestNode = closestGraphViewNode != null ? (Node<ExpressionBuilder, ExpressionBuilderParameter>)closestGraphViewNode.Tag : null;
                if (elementType == LoadableElementType.Source)
                {
                    if (closestNode != null && !(closestNode.Value is SourceBuilder) && !workflowBuilder.Workflow.Predecessors(closestNode).Any())
                    {
                        var parameter = new ExpressionBuilderParameter("Source");
                        addConnection = () => workflowBuilder.Workflow.AddEdge(node, closestNode, parameter);
                        removeConnection = () => workflowBuilder.Workflow.RemoveEdge(node, closestNode, parameter);
                    }
                }
                else if (closestNode != null)
                {
                    if (!branch)
                    {
                        var oldSuccessor = closestNode.Successors.FirstOrDefault();
                        if (oldSuccessor.Node != null)
                        {
                            //TODO: Decide when to insert or branch
                            addConnection = () =>
                            {
                                workflowBuilder.Workflow.RemoveEdge(closestNode, oldSuccessor.Node, oldSuccessor.Label);
                                workflowBuilder.Workflow.AddEdge(node, oldSuccessor.Node, oldSuccessor.Label);
                            };

                            removeConnection = () =>
                            {
                                workflowBuilder.Workflow.RemoveEdge(node, oldSuccessor.Node, oldSuccessor.Label);
                                workflowBuilder.Workflow.AddEdge(closestNode, oldSuccessor.Node, oldSuccessor.Label);
                            };
                        }
                    }

                    var insertSuccessor = addConnection;
                    var removeSuccessor = removeConnection;
                    var parameter = new ExpressionBuilderParameter("Source");
                    addConnection = () => { insertSuccessor(); workflowBuilder.Workflow.AddEdge(closestNode, node, parameter); };
                    removeConnection = () => { workflowBuilder.Workflow.RemoveEdge(closestNode, node, parameter); removeSuccessor(); };
                }

                commandExecutor.Execute(
                () =>
                {
                    addNode();
                    addConnection();
                    UpdateGraphLayout();
                    workflowGraphView.SelectedNode = workflowGraphView.Nodes.SelectMany(layer => layer).First(n => n.Tag == node);
                },
                () =>
                {
                    removeConnection();
                    removeNode();
                    UpdateGraphLayout();
                    workflowGraphView.SelectedNode = workflowGraphView.Nodes.SelectMany(layer => layer).FirstOrDefault(n => n.Tag == closestNode);
                });
            }
        }

        void DeleteGraphNode(GraphNode node)
        {
            if (running == null && node != null)
            {
                Action addEdge = () => { };
                Action removeEdge = () => { };

                var workflowNode = (Node<ExpressionBuilder, ExpressionBuilderParameter>)node.Tag;
                var predecessorEdges = workflowBuilder.Workflow.PredecessorEdges(workflowNode).ToArray();
                var sourcePredecessor = Array.Find(predecessorEdges, edge => edge.Item2.Label.Value == "Source");
                if (sourcePredecessor != null)
                {
                    addEdge = () =>
                    {
                        foreach (var successor in workflowNode.Successors)
                        {
                            if (workflowBuilder.Workflow.Successors(sourcePredecessor.Item1).Contains(successor.Node)) continue;
                            workflowBuilder.Workflow.AddEdge(sourcePredecessor.Item1, successor.Node, successor.Label);
                        }
                    };

                    removeEdge = () =>
                    {
                        foreach (var successor in workflowNode.Successors)
                        {
                            workflowBuilder.Workflow.RemoveEdge(sourcePredecessor.Item1, successor.Node, successor.Label);
                        }
                    };
                }

                Action removeNode = () => workflowBuilder.Workflow.Remove(workflowNode);
                Action addNode = () =>
                {
                    workflowBuilder.Workflow.Add(workflowNode);
                    foreach (var edge in predecessorEdges)
                    {
                        edge.Item1.Successors.Insert(edge.Item3, edge.Item2);
                    }
                };

                commandExecutor.Execute(
                () =>
                {
                    addEdge();
                    removeNode();
                    UpdateGraphLayout();
                    workflowGraphView.SelectedNode = workflowGraphView.Nodes.SelectMany(layer => layer).FirstOrDefault(n => n.Tag != null && n.Tag == sourcePredecessor);
                },
                () =>
                {
                    addNode();
                    removeEdge();
                    UpdateGraphLayout();
                    workflowGraphView.SelectedNode = workflowGraphView.Nodes.SelectMany(layer => layer).FirstOrDefault(n => n.Tag == node.Tag);
                });
            }
        }

        void StartWorkflow()
        {
            if (running == null)
            {
                runningWorkflow = workflowBuilder.Workflow.ToInspectableGraph();
                try
                {
                    var subscribeExpression = runningWorkflow.BuildSubscribe(HandleWorkflowError);

                    visualizerMapping = (from node in runningWorkflow
                                         where !(node.Value is InspectBuilder)
                                         let inspectBuilder = node.Successors.First().Node.Value as InspectBuilder
                                         where inspectBuilder != null
                                         let nodeName = builderConverter.ConvertToString(node.Value)
                                         select new { node, nodeName, inspectBuilder })
                                        .ToDictionary(mapping => workflowGraphView.Nodes.SelectMany(layer => layer).First(node => node.Value == mapping.node.Value),
                                                      mapping =>
                                                      {
                                                          Type visualizerType;
                                                          if (!typeVisualizers.TryGetValue(mapping.inspectBuilder.ObservableType, out visualizerType))
                                                          {
                                                              visualizerType = typeVisualizers[typeof(object)];
                                                          };

                                                          var visualizer = (DialogTypeVisualizer)Activator.CreateInstance(visualizerType);
                                                          return mapping.inspectBuilder.CreateVisualizerDialog(mapping.nodeName, visualizer, editorSite);
                                                      });

                    loaded = workflowBuilder.Load();

                    var subscriber = subscribeExpression.Compile();
                    var sourceConnections = workflowBuilder.GetSources().Select(source => source.Connect());
                    running = new CompositeDisposable(Enumerable.Repeat(subscriber(), 1).Concat(sourceConnections));
                }
                catch (InvalidOperationException ex) { HandleWorkflowError(ex); return; }
                catch (ArgumentException ex) { HandleWorkflowError(ex); return; }
            }

            runningStatusLabel.Text = Resources.RunningStatus;
        }

        void StopWorkflow()
        {
            if (running != null)
            {
                foreach (var visualizerDialog in visualizerMapping.Values)
                {
                    visualizerDialog(false);
                }

                running.Dispose();
                loaded.Dispose();
                loaded = null;
                running = null;
                runningWorkflow = null;
                visualizerMapping = null;
            }

            runningStatusLabel.Text = Resources.StoppedStatus;
        }

        void HandleWorkflowError(Exception e)
        {
            MessageBox.Show(e.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (running != null)
            {
                BeginInvoke((Action)StopWorkflow);
            }
        }

        void UpdateGraphLayout()
        {
            workflowGraphView.Nodes = workflowBuilder.Workflow.LongestPathLayering().AverageMinimizeCrossings();
            workflowGraphView.Invalidate();
        }

        #endregion

        #region Workflow Controller

        private void toolboxTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var selectedNode = e.Item as TreeNode;
            if (selectedNode != null && selectedNode.GetNodeCount(false) == 0)
            {
                toolboxTreeView.DoDragDrop(selectedNode.Name, DragDropEffects.Copy);
            }
        }

        private void workflowGraphView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(typeof(GraphNode)))
            {
                e.Effect = DragDropEffects.Link;
            }
            else e.Effect = DragDropEffects.None;
        }

        private void workflowGraphView_DragDrop(object sender, DragEventArgs e)
        {
            var dropLocation = workflowGraphView.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Copy)
            {
                var typeName = e.Data.GetData(DataFormats.Text).ToString();
                var closestGraphViewNode = workflowGraphView.GetClosestNodeTo(dropLocation);
                CreateGraphNode(typeName, closestGraphViewNode, (e.KeyState & CtrlModifier) != 0);
            }

            if (e.Effect == DragDropEffects.Link)
            {
                var linkNode = workflowGraphView.GetNodeAt(dropLocation);
                if (linkNode != null)
                {
                    var node = (GraphNode)e.Data.GetData(typeof(GraphNode));
                    ConnectGraphNodes(node, linkNode);
                }
            }
        }

        private void workflowGraphView_SelectedNodeChanged(object sender, EventArgs e)
        {
            var node = workflowGraphView.SelectedNode;
            if (node != null && node.Value != null)
            {
                var loadableElement = node.Value.GetType().GetProperties().FirstOrDefault(property => typeof(LoadableElement).IsAssignableFrom(property.PropertyType));
                if (loadableElement != null)
                {
                    propertyGrid.SelectedObject = loadableElement.GetValue(node.Value, null);
                }
                else propertyGrid.SelectedObject = node.Value;
            }
            else propertyGrid.SelectedObject = null;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartWorkflow();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopWorkflow();
        }

        private void workflowGraphView_NodeMouseDoubleClick(object sender, GraphNodeMouseClickEventArgs e)
        {
            if (running != null)
            {
                var visualizerDialog = visualizerMapping[e.Node];
                visualizerDialog(true);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = workflowGraphView.SelectedNode;
            DeleteGraphNode(node);
        }

        private void toolboxTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && toolboxTreeView.SelectedNode != null && toolboxTreeView.SelectedNode.GetNodeCount(false) == 0)
            {
                var typeName = toolboxTreeView.SelectedNode.Name;
                CreateGraphNode(typeName, workflowGraphView.SelectedNode, e.Modifiers == Keys.Control);
            }
        }

        private void workflowGraphView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var selectedNode = e.Item as GraphNode;
            if (selectedNode != null)
            {
                workflowGraphView.DoDragDrop(selectedNode, DragDropEffects.Link);
            }
        }

        private void toolboxTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Node.GetNodeCount(false) == 0)
            {
                var typeName = e.Node.Name;
                CreateGraphNode(typeName, workflowGraphView.SelectedNode, Control.ModifierKeys == Keys.Control);
            }
        }

        #endregion

        #region Undo/Redo

        private void commandExecutor_StatusChanged(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = commandExecutor.CanUndo;
            redoToolStripMenuItem.Enabled = commandExecutor.CanRedo;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandExecutor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandExecutor.Redo();
        }

        #endregion

        #region Help Menu

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var about = new AboutBox())
            {
                about.ShowDialog();
            }
        }

        #endregion

        #region EditorSite Class

        class EditorSite : ISite
        {
            MainForm siteForm;

            public EditorSite(MainForm form)
            {
                siteForm = form;
            }

            public IComponent Component
            {
                get { return null; }
            }

            public IContainer Container
            {
                get { return null; }
            }

            public bool DesignMode
            {
                get { return false; }
            }

            public string Name { get; set; }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(ExpressionBuilderGraph))
                {
                    return siteForm.runningWorkflow;
                }

                return null;
            }
        }

        #endregion
    }
}
