using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int>> add_exp = (a) =>(a + 1);
            Expression<Func<int, int>> sub_exp = (a) => (a - 1);

            var result_addexp = (new Transformator().VisitAndConvert(add_exp, ""));
            var result_subexp = (new Transformator().VisitAndConvert(sub_exp, ""));

            Console.WriteLine(add_exp + " = " + add_exp.Compile().Invoke(3));
            Console.WriteLine(result_addexp + " = " + result_addexp.Compile().Invoke(3));

            Console.WriteLine(sub_exp + " = " + sub_exp.Compile().Invoke(3));
            Console.WriteLine(result_subexp + " = " + result_subexp.Compile().Invoke(3));

            var result_transform = (new Transformator().ChangeParameter(
                                           add_exp,
                                           new Dictionary<string, int>() { {"a", 1 } }));
            Console.WriteLine(result_transform + " = " + result_transform.Compile().Invoke(1));
            Console.ReadLine();
        }
    }
}
