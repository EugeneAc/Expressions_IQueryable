using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Task_1_Test
{
    using Task_1;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIncrement()
        {
            Expression<Func<int, int>> add_exp = (a) => (a + 1);
            var result_addexp = (new Transformator().VisitAndConvert(add_exp, ""));
            var result = result_addexp.Compile().Invoke(3);
            Console.WriteLine(result_addexp + " = " + result);
            Assert.IsTrue(result_addexp.Body.ToString().Contains("Increment"));
        }
        [TestMethod]
        public void TestDecrement()
        {
            Expression<Func<int, int>> sub_exp = (a) => (a - 1);
            var result_subexp = (new Transformator().VisitAndConvert(sub_exp, ""));
            var result = sub_exp.Compile().Invoke(3);
            Console.WriteLine(sub_exp + " = " + result);
            Assert.IsTrue(result_subexp.Body.ToString().Contains("Decrement"));

        }
        [TestMethod]
        public void TestTransform()
        {
            Expression<Func<int, int>> add_exp = (a) => (a + 1);

            var result_transform = (new Transformator().ChangeParameter(
                                           add_exp,
                                           new Dictionary<string, int>() { { "a", 1 } }));
            var result = result_transform.Compile().Invoke(1);
            Console.WriteLine(result_transform + " = " + result);
            Assert.IsTrue(result == 2);
        }
    }
}
