
namespace Task_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Mapper<TSource, TDestination>
    {
        private Func<TSource, TDestination> _mapFunction;

        private Dictionary<Func<TSource, object>, PropertyInfo> _mappings = new Dictionary<Func<TSource, object>, PropertyInfo>();

        internal Mapper(Func<TSource, TDestination> func)
        {
            _mapFunction = func;
        }

        public TDestination Map(TSource source)
        {
            var r = _mapFunction(source);
            foreach (var func in _mappings)
            {
                var s = func.Key.Invoke(source);
                func.Value.SetValue(r, s);
            }
            return r;
        }

        public Mapper<TSource, TDestination> CopyParam(Expression<Func<TSource, object>> sourceExpression, Expression<Func<TDestination, object>> destinationExpression)
        {
            PropertyInfo destinationProperty = null;

            if (sourceExpression.Body is MemberExpression)
            {
                destinationProperty = ((MemberExpression)destinationExpression.Body).Member as PropertyInfo;
            }
            if (sourceExpression.Body is UnaryExpression)
            {
                destinationProperty = ((MemberExpression)((UnaryExpression)destinationExpression.Body).Operand).Member as PropertyInfo;
            }
            var sourceDelegate = sourceExpression.Compile();
            _mappings.Add(sourceDelegate, destinationProperty);
            return this;
        }
    }
}
