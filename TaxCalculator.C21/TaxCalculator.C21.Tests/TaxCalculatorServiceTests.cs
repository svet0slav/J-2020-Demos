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
            Assert.AreEqual(3000m, salary.TaxExplanation.Last().Key.FromAmountIncluding);
            Assert.AreEqual(240m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(300m, salary.TaxExplanation.Last().Value);
            Assert.AreEqual(540m, salary.TaxAmount);
            Assert.AreEqual(salary.GrossAmount - 540m, salary.NetAmount);
        }

        [TestMethod]
        public void Service_CaseMiddle_Calculate()
        {
            var employee = CreateEmployee();
            var salary = CreateSalary(employee, DateTime.Now, 2400m);
            var service = CreateService();

            service.CalculateTax(salary);

            Assert.AreEqual(1000m, salary.TaxDefinition.Definitions.First().UpToAmount);
            Assert.IsNotNull(salary.TaxExplanation);
            Assert.AreEqual(2, salary.TaxExplanation.Count);
            Assert.AreEqual(1000m, salary.TaxExplanation.First().Key.FromAmountIncluding);
            Assert.AreEqual(1000m, salary.TaxExplanation.Last().Key.FromAmountIncluding);
            Assert.AreEqual(140m, salary.TaxExplanation.First().Value);
            Assert.AreEqual(210m, salary.TaxExplanation.Last().Value);
            Assert.AreEqual(350m, salary.TaxAmount);
            Assert.AreEqual(salary.GrossAmount - 350m, salary.NetAmount);
        }

        [TestMethod]
        public void Service_CasesMany_Calculate()
        {
            // Alternative to XUnit Inline and Theory.
            Service_CaseOne_Calculate(DateTime.Now, 500m, 0m);
            Service_CaseOne_Calculate(DateTime.Now, 900m, 0m);
            Service_CaseOne_Calculate(DateTime.Now, 1000m, 0m);
            Service_CaseOne_Calculate(DateTime.Now, 1500m, 50m + 75m);
            Service_CaseOne_Calculate(DateTime.Now, 2000m, 100m + 150m);
            Service_CaseOne_Calculate(DateTime.Now, 2500m, 150m + 225m);
            Service_CaseOne_Calculate(DateTime.Now, 3000m, 200m + 300m);
            Service_CaseOne_Calculate(DateTime.Now, 3500m, 250m + 300m);
            Service_CaseOne_Calculate(DateTime.Now, 5000m, 400m + 300m);
        }


        private void Service_CaseOne_Calculate(DateTime when, decimal grossAmount, decimal expectedTaxAmount)
        {
            var employee = CreateEmployee();
            var salary = CreateSalary(employee, when, grossAmount);
            var service = CreateService();

            service.CalculateTax(salary);

            Assert.IsNotNull(salary.TaxDefinition);
            if (expectedTaxAmount > 0)
            {
                Assert.IsNotNull(salary.TaxExplanation);
            }
            Assert.AreEqual(expectedTaxAmount, salary.TaxAmount);
            Assert.AreEqual(salary.GrossAmount - expectedTaxAmount, salary.NetAmount);
        }

        private Employee CreateEmployee()
        {
            return new Mock<Employee>().Object; // new Employee();
        }

        private Salary CreateSalary(Employee employee, DateTime moment, decimal grossAmount)
        {
            var salary = new Mock<Salary>().Object;
            salary.GrossAmount = grossAmount;
            return salary;
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
