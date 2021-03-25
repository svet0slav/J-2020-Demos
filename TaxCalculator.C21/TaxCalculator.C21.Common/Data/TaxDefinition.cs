using System;
using System.Collections.Generic;

namespace TaxCalculator.C21.Common.Data
{
    public class TaxDefinition
    {
        public int Id { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }

        /// <summary>
        /// This definition applies from amount, including it.
        /// </summary>
        public IEnumerable<TaxDefinitionItem> Definitions { get; set; }
    }
}
