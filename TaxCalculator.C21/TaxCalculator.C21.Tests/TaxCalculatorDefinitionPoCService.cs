using System;
using System.Collections.Generic;
using TaxCalculator.C21.Common.Data;
using TaxCalculator.C21.Services;

namespace TaxCalculator.C21.Tests
{
    public class TaxCalculatorDefinitionPoCService : ITaxCalculatorDefinitionService
    {
        public TaxDefinition GetTaxDefinition(DateTime moment)
        {
            //This is prove of concept. Hard-coded data.
            var result = new TaxDefinition()
            {
                Id = 0,
                //First and last day of the month
                PeriodFrom = new DateTime(moment.Year, moment.Month, 1),
                PeriodTo = new DateTime(moment.Year, moment.Month, moment.AddMonths(1).AddDays(-1).Day),
                Definitions = new List<TaxDefinitionItem>()
                {
                    new TaxDefinitionItem()
                    {
                        Name = "Tax does not apply",
                        FromAmountIncluding = 0m,
                        UpToAmount = 1000m,
                        BaseTaxAmount = 0,
                        PercentAboveFrom = 0,
                    },
                    new TaxDefinitionItem()
                    {
                        Name = "Tax 10%",
                        FromAmountIncluding = 1000m,
                        UpToAmount = decimal.MaxValue,
                        BaseTaxAmount = 0m,
                        PercentAboveFrom = 0.10m
                    },
                    new TaxDefinitionItem()
                    {
                        Name = "Social contribution",
                        FromAmountIncluding = 1000m,
                        UpToAmount = 3000m,
                        BaseTaxAmount = 0m,
                        PercentAboveFrom = 0.15m
                    }
                }
            };

            return result;
        }
    }
}
