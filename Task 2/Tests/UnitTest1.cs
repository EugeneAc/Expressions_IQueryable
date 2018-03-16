using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using System.Configuration;
    using System.Linq;

    using Sample03;
    using Sample03.E3SClient.Entities;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WithProviderLeftConstant()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => "EPRUIZHW0249" == e.workstation))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }
        [TestMethod]
        public void WithProviderStarWith()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            Assert.IsTrue(employees.Where(e => e.workstation.StartsWith("EPRUIZHW02")).ToArray().Length > 0);

            foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPRUIZHW02")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderEndWith()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            Assert.IsTrue(employees.Where(e => e.workstation.EndsWith("249")).ToArray().Length > 0);

            foreach (var emp in employees.Where(e => e.workstation.EndsWith("249")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderContains()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            Assert.IsTrue(employees.Where(e => e.workstation.Contains("EPRUIZ")).ToArray().Length > 0);

            foreach (var emp in employees.Where(e => e.workstation.Contains("EPRUIZ")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }

        [TestMethod]
        public void WithProviderAnd()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            Assert.IsTrue(employees.Where(e => e.workstation.Contains("EPR") && e.workstation.EndsWith("249")).ToArray().Length > 0);

            foreach (var emp in employees.Where(e => e.workstation.Contains("EPR") && e.workstation.EndsWith("249")))
            {
                Console.WriteLine("{0} {1}", emp.nativename, emp.shortStartWorkDate);
            }
        }
    }
}
