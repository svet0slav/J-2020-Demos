using System.Collections.Generic;

namespace TaxCalculator.C21.Common.Data
{
    public class Salaries
    {
        public int Id { get; set; }
        public IEnumerable<Salary> Items { get; set; }
    }
}
