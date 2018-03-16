using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    using System.Linq.Expressions;

    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var mapFunction =
                Expression.Lambda<Func<TSource, TDestination>>(
                    Expression.New(typeof(TDestination)),
                    sourceParam);
            var func = new Mapper<TSource, TDestination>(mapFunction.Compile());
            return func;
        }
    }
}
