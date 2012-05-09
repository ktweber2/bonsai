﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using Bonsai.Expressions;
using Bonsai.Dag;
using System.Collections.ObjectModel;

namespace Bonsai.Design
{
    public class MemberSelectorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var selector = value as Collection<string>;
            var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (selector != null && context != null && editorService != null)
            {
                var workflowBuilder = (WorkflowBuilder)provider.GetService(typeof(WorkflowBuilder));
                if (workflowBuilder == null) return base.EditValue(context, provider, value);

                var nodeBuilderGraph = (ExpressionBuilderGraph)provider.GetService(typeof(ExpressionBuilderGraph));
                if (nodeBuilderGraph == null) return base.EditValue(context, provider, value);

                var workflow = workflowBuilder.Workflow;
                var predecessorNode = (from node in nodeBuilderGraph
                                       let builder = node.Value
                                       where builder == context.Instance
                                       select nodeBuilderGraph.Predecessors(node).SingleOrDefault()).SingleOrDefault();

                if (predecessorNode == null) return base.EditValue(context, provider, value);
                workflow.Build(); //TODO: Find a better way to ensure type inference on arbitrary nested workflows

                var expressionType = predecessorNode.Value.Build().Type.GetGenericArguments()[0];
                var editorDialog = new MemberSelectorEditorDialog(expressionType, selector);
                if (editorService.ShowDialog(editorDialog) == DialogResult.OK)
                {
                    selector.Clear();
                    foreach (var memberName in editorDialog.GetMemberChain())
                    {
                        selector.Add(memberName);
                    }

                    return selector;
                }
            }

            return base.EditValue(context, provider, value);
        }
    }
}
