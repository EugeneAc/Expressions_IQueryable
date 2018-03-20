namespace Tests
{
    using System;
    using System.Configuration;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sample03;
    using Sample03.E3SClient.Entities;

    [TestClass]
    public class QueryableTests
    {
        private E3SEntitySet<EmployeeEntity> _employees;
        [TestInitialize]
        public void SetupContext()
        {
            _employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
        }

        [TestMethod]
        public void WithProviderLeftConstant()
        {
            foreach (var emp in _employees.Where(e => "EPRUIZHW0249" == e.workstation))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderStarWith()
        {
            var result = _employees.Where(e => e.workstation.StartsWith("EPRUIZHW02"));
            Assert.IsTrue(result.ToArray().Length > 0);

            foreach (var emp in result)
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderEndWith()
        {
            var result = _employees.Where(e => e.workstation.EndsWith("249"));
            Assert.IsTrue(result.ToArray().Length > 0);

            foreach (var emp in result)
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderContains()
        {
            var result = _employees.Where(e => e.workstation.Contains("EPRUIZ"));
            Assert.IsTrue(result.ToArray().Length > 0);

            foreach (var emp in result)
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderAnd()
        {
            var result = _employees.Where(e => e.workstation.Contains("EPR") && e.workstation.EndsWith("249"));
            Assert.IsTrue(result.ToArray().Length > 0);

            foreach (var emp in result)
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }
    }
}
