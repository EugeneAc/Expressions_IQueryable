using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_2_Test
{
    using Task_2;
    [TestClass]
    public class UnitTest1
    {
        public class Foo
        {
            public string FooStringProp
            {
                get => "test";
            }
            public int FooIntProp
            {
                get => 1234;
            }
        }
        public class Bar
        {
            public string BarStringProp { get; set; }
            public int BarIntProp { get; set; }
        }
        [TestMethod]
        public void TestNotNull()
        {
            MappingGenerator mapGenerator = new MappingGenerator();
            Bar bar = mapGenerator.Generate<Foo, Bar>().Map(new Foo());
                Assert.IsNotNull(bar);
            Console.WriteLine(bar);
        }
        [TestMethod]
        public void TestRefMapping()
        {
            MappingGenerator mapGenerator = new MappingGenerator();
            Bar bar = mapGenerator.Generate<Foo, Bar>().CopyParam(f=>f.FooStringProp, b => b.BarStringProp).Map(new Foo());
            Assert.IsTrue(bar.BarStringProp == "test");
            Console.WriteLine(bar);
            Console.WriteLine(bar.BarStringProp);
        }
        [TestMethod]
        public void TestValueMapping()
        {
            MappingGenerator mapGenerator = new MappingGenerator();
            Bar bar = mapGenerator.Generate<Foo, Bar>().CopyParam(f => f.FooIntProp, b => b.BarIntProp).Map(new Foo());
            Assert.IsTrue(bar.BarIntProp == 1234);
            Console.WriteLine(bar);
            Console.WriteLine(bar.BarIntProp);
        }
    }
}



