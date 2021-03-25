using System;
using SConsole = System.Console;
using TaxCalculator.C21.Services;
using TaxCalculator.C21.Common.Data;

namespace TaxCalculator.C21.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SConsole.WriteLine("Hello, enter gross amount:");
            var amountText = SConsole.ReadLine();
            if (decimal.TryParse(amountText, out decimal amount))
            {
                var service = new TaxCalculatorService(new TaxCalculatorDefinitionPoCService());
                var salary = new Salary() { GrossAmount = amount };
                service.CalculateTax(salary);
                SConsole.WriteLine($"Gross amount : {salary.GrossAmount:N2}");
                SConsole.WriteLine($"The tax is {salary.TaxAmount:N2}");
                if (salary.TaxExplanation != null)
                {
                    foreach(var item in salary.TaxExplanation)
                    {
                        SConsole.WriteLine($"includes {item.Key.Name} with tax {item.Value:N2}");
                    }
                }
                SConsole.WriteLine($"Net amount : {salary.NetAmount}");
            }

            SConsole.WriteLine("Finished. Press ENTER to exit!");
            SConsole.ReadLine();
        }
    }
}
