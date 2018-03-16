using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    using System.Linq.Expressions;

    public class Transformator : ExpressionVisitor
    {
        private Dictionary<string, int> _changeArgs;

        public Expression<T> ChangeParameter<T>(Expression<T> exp, Dictionary<string, int> changeArgs)
        {
            _changeArgs = changeArgs;
            var test = new Transformator().VisitAndConvert(exp, string.Empty);
            return test;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;
                if (node.Left.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Left;
                }
                else if (node.Left.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Left;
                }

                if (node.Right.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Right;
                }
                else if (node.Right.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Right;
                }

                if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    return Expression.Increment(param);
                }
            }

            if (node.NodeType == ExpressionType.Subtract)
            {
                ParameterExpression param = null;
                ConstantExpression constant = null;
                if (node.Left.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Left;
                }
                else if (node.Left.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Left;
                }

                if (node.Right.NodeType == ExpressionType.Parameter)
                {
                    param = (ParameterExpression)node.Right;
                }
                else if (node.Right.NodeType == ExpressionType.Constant)
                {
                    constant = (ConstantExpression)node.Right;
                }

                if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    return Expression.Decrement(param);
                }
            }

            return base.VisitBinary(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_changeArgs != null)
            {
                foreach (var pArg in _changeArgs)
                {
                    if (node.Name == pArg.Key)
                        return Expression.Constant(pArg.Value);
                }
            }
            return base.VisitParameter(node);
        }
    }
}
