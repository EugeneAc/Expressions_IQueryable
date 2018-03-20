namespace Task_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Transformator : ExpressionVisitor
    {
        private Dictionary<string, int> _changeArgs;

        public Expression ChangeParameter<T>(Expression<T> exp, Dictionary<string, int> changeArgs)
        {
            _changeArgs = changeArgs;
           var test = VisitLambda(exp);
            return test;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var nb = VisitAndConvert(node.Body, string.Empty);

            return Expression.Lambda(nb,node.Parameters);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add)
            {
                var prm = SetParamAndConstant(node);
                var param = prm.Item1;
                var constant = prm.Item2;

                if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                {
                    return Expression.Increment(param);
                }
            }
            
            if (node.NodeType == ExpressionType.Subtract)
            {
                var prm = SetParamAndConstant(node);
                var param = prm.Item1;
                var constant = prm.Item2;

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
                    {
                        return Expression.Constant(pArg.Value);
                    }
                }
            }

            return base.VisitParameter(node);
        }

        private Tuple<ParameterExpression, ConstantExpression> SetParamAndConstant(BinaryExpression node)
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

            return new Tuple<ParameterExpression, ConstantExpression>(param, constant);
        }
    }
}
