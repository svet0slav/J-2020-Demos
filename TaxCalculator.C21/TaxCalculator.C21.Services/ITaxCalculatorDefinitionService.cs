using System;
using TaxCalculator.C21.Common.Data;

namespace TaxCalculator.C21.Services
{
    public interface ITaxCalculatorDefinitionService
    {
        TaxDefinition GetTaxDefinition(DateTime moment);
    }
}