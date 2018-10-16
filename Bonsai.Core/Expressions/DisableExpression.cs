﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Expressions
{
    class DisableExpression : Expression
    {
        readonly IEnumerable<Expression> arguments;

        public DisableExpression(IEnumerable<Expression> arguments)
        {
            this.arguments = arguments;
        }

        public override ExpressionType NodeType
        {
            get { return ExpressionType.Extension; }
        }

        public override Type Type
        {
            get { throw new InvalidOperationException("Unable to evaluate disabled expression. Ensure there are no conflicting inputs to disabled nodes."); }
        }

        public IEnumerable<Expression> Arguments
        {
            get
            {
                foreach (var argument in arguments)
                {
                    var disable = argument as DisableExpression;
                    if (disable != null)
                    {
                        foreach (var nestedArgument in disable.Arguments)
                        {
                            yield return nestedArgument;
                        }
                    }
                    else yield return argument;
                }
            }
        }
    }
}
