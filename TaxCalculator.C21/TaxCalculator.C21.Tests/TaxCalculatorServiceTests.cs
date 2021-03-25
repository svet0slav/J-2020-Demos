using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
            var employee = new Employee();
            var salary = CreateSalary(employee, DateTime.Now, 980m);
            var defService = CreateDefinitionService();
            var service = CreateService();

            var definition = CreateDefinition(defService);
            service.CalculateTax(salary, definition);

            Assert.AreEqual(1000m, definition.Definitions.First().UpToAmount);
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
            var defService = CreateDefinitionService();
            var service = CreateService();

            var definition = CreateDefinition(defService);
            service.CalculateTax(salary, definition);

            Assert.AreEqual(1000m, definition.Definitions.First().UpToAmount);
            Assert.IsNotNull(salary.TaxExplanation);
            Assert.AreEqual(2, salary.TaxExplanation.Count);
            Assert.AreEqual(1000m, salary.TaxExplanation.First().Key.FromAmountIncluding);
            Assert.AreEqual(2000m, salary.TaxExplanation.Last().Key.FromAmountIncluding);
            Assert.AreEqual(240m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(300m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(540m, salary.TaxAmount);
            Assert.AreEqual(salary.GrossAmount - 540m, salary.NetAmount);
        }

        private ITaxCalculatorService CreateService()
        {
            return new TaxCalculatorService();
        }
        private ITaxCalculatorDefinitionService CreateDefinitionService()
        {
            return new TaxCalculatorDefinitionPoCService();
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

        private TaxDefinition CreateDefinition(ITaxCalculatorDefinitionService definitionService)
        {
            return definitionService.GetTaxDefinition(DateTime.Now);
        }
    }
}
