using TaxCalculator.C21.Common.Data;

namespace TaxCalculator.C21.Services
{
    public interface ITaxCalculatorService
    {
        /// <summary>
        /// Calculate tax and net amount for a salary.
        /// </summary>
        /// <param name="salary">The definition of the salary.</param>
        void CalculateTax(Salary salary);
    }
}