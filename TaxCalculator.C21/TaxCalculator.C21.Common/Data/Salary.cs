using System;
using System.Collections.Generic;

namespace TaxCalculator.C21.Common.Data
{
    /// <summary>
    /// Complex class for the salary in a period.
    /// </summary>
    public class Salary
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }

        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public IDictionary<TaxDefinitionItem, decimal> TaxExplanation { get; set; }
        public decimal NetAmount { get; set; }
    }
}
