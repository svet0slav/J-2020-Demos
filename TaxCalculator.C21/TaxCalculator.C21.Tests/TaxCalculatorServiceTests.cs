using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Moq;
using TaxCalculator.C21.Common.Data;
using TaxCalculator.C21.Services;

namespace TaxCalculator.C21.Tests
{
    [TestClass]
    public class TaxCalculatorServiceTests
    {
        [TestMethod]
        public void Service_CaseGeorge_Calculate()
        {
            var employee = CreateEmployee();
            var salary = CreateSalary(employee, DateTime.Now, 980m);
            var service = CreateService();

            service.CalculateTax(salary);

            Assert.AreEqual(1000m, salary.TaxDefinition.Definitions.First().UpToAmount);
            Assert.IsNotNull(salary.TaxExplanation);
            Assert.AreEqual(1, salary.TaxExplanation.Count);
            Assert.AreEqual(1000m, salary.TaxExplanation.First().Key.UpToAmount);
            Assert.AreEqual(0m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(0m, salary.TaxAmount);
            Assert.AreEqual(salary.GrossAmount, salary.NetAmount);
        }

        [TestMethod]
        public void Service_CaseIrina_Calculate()
        {
            var employee = new Employee();
            var salary = CreateSalary(employee, DateTime.Now, 3400m);
            var service = CreateService();

            service.CalculateTax(salary);

            Assert.AreEqual(1000m, salary.TaxDefinition.Definitions.First().UpToAmount);
            Assert.IsNotNull(salary.TaxExplanation);
            Assert.AreEqual(2, salary.TaxExplanation.Count);
            Assert.AreEqual(1000m, salary.TaxExplanation.First().Key.FromAmountIncluding);
            Assert.AreEqual(2000m, salary.TaxExplanation.Last().Key.FromAmountIncluding);
            Assert.AreEqual(240m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(300m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(540m, salary.TaxAmount);
            Assert.AreEqual(salary.GrossAmount - 540m, salary.NetAmount);
        }

        private Employee CreateEmployee()
        {
            return new Mock<Employee>().Object; // new Employee();
        }

        private Salary CreateSalary(Employee employee, DateTime moment, decimal grossAmount)
        {
            return new Salary()
            {
                Id = 0,
                Employee = employee,
                PeriodFrom = moment,
                PeriodTo = moment,
                GrossAmount = grossAmount
            };
        }

        private ITaxCalculatorDefinitionService CreateDefinitionService()
        {
            return new TaxCalculatorDefinitionPoCService();
        }

        private ITaxCalculatorService CreateService()
        {
            var definitionService = CreateDefinitionService();
            return new TaxCalculatorService(definitionService);
        }
    }
}
