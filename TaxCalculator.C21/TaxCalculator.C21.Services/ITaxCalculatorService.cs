using TaxCalculator.C21.Common.Data;

namespace TaxCalculator.C21.Services
{
    public interface ITaxCalculatorService
    {
        void CalculateTax(Salary salary, TaxDefinition taxDefinition);
    }
}