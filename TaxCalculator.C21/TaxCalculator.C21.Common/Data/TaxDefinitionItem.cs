namespace TaxCalculator.C21.Common.Data
{
    public class TaxDefinitionItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// This definition applies from amount, including it.
        /// </summary>
        public decimal FromAmountIncluding { get; set; }

        /// <summary>
        /// This definition applies up to amount, excluding it.
        /// </summary>
        public decimal UpToAmount { get; set; }
        public decimal BaseTaxAmount { get; set; }
        public decimal PercentAboveFrom { get; set; }
    }
}
