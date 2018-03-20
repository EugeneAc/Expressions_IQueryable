

namespace Task_1_Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Task_1;

    [TestClass]
    public class Task1Tests
    {
        [TestMethod]
        public void TestIncrement()
        {
            Expression<Func<int, int>> add_exp = (a) => (a + 1);
            var result_addexp = new Transformator().VisitAndConvert(add_exp, string.Empty);
            var result = result_addexp.Compile().Invoke(3);
            Console.WriteLine(result_addexp + " = " + result);
            Assert.IsTrue(result_addexp.Body.ToString().Contains("Increment"));
            Assert.IsTrue(result == 4);
        }

        [TestMethod]
        public void TestDecrement()
        {
            Expression<Func<int, int>> sub_exp = (a) => (a - 1);
            var result_subexp = new Transformator().VisitAndConvert(sub_exp, string.Empty);
            var result = sub_exp.Compile().Invoke(3);
            Console.WriteLine(sub_exp + " = " + result);
            Assert.IsTrue(result_subexp.Body.ToString().Contains("Decrement"));
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void TestTransform()
        {
            Expression<Func<int, int>> add_exp = (a) => (a + 5);
            var result_transform = new Transformator().ChangeParameter(
                                           add_exp,
                                           new Dictionary<string, int>() { { "a", 1 } });
           Console.WriteLine(result_transform.ToString());
           Assert.IsTrue(result_transform.ToString().ToCharArray()[6] == Convert.ToChar("1"));
        }
    }
}
