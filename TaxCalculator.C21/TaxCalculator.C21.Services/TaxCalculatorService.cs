﻿using System;
using System.Collections.Generic;
using TaxCalculator.C21.Common.Data;

namespace TaxCalculator.C21.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private ITaxCalculatorDefinitionService _definitionService;

        public TaxCalculatorService(ITaxCalculatorDefinitionService definitionService)
        {
            _definitionService = definitionService;
        }

        public void CalculateTax(Salary salary)
        {
            if (salary == null)
                throw new ArgumentException("Salary definition unknwon");

            var taxDefinition = GetDefinition(salary.PeriodFrom);
            salary.TaxDefinition = taxDefinition;
            salary.TaxAmount = 0;
            if (salary.TaxExplanation != null)
                salary.TaxExplanation = null;

            if (taxDefinition.Definitions != null)
            {
                foreach (var item in taxDefinition.Definitions)
                {
                    var apply = AppliesTaxItem(salary.GrossAmount, item, out decimal taxAmount);
                    if (apply)
                    {
                        AddExplanation(salary, item, taxAmount);
                    }
                }
            }
            salary.NetAmount = salary.GrossAmount - salary.TaxAmount;
        }

        protected TaxDefinition GetDefinition(DateTime moment)
        {
            var result = _definitionService.GetTaxDefinition(moment);
            if (result == null)
                throw new Exception("No tax definition");
            return result;
        }

        /// <summary>
        /// Define whether tax definition applies and return calculated amount.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="taxAmount"></param>
        /// <returns></returns>
        protected bool AppliesTaxItem(decimal grossAmount, TaxDefinitionItem item, out decimal taxAmount)
        {
            if (item == null)
                throw new ArgumentException("Undefined tax item");

            taxAmount = 0m;
            if (grossAmount >= item.FromAmountIncluding && grossAmount < item.UpToAmount)
            {
                if (item.UpToAmount >= TaxDefinitionItem.MaxUpToAmount)
                {
                    taxAmount = item.BaseTaxAmount + (grossAmount - item.FromAmountIncluding) * item.PercentAboveFrom;
                }
                else
                {
                    taxAmount = item.BaseTaxAmount;
                    if (grossAmount >= item.UpToAmount)
                    {
                        taxAmount += (item.UpToAmount - item.FromAmountIncluding) * item.PercentAboveFrom;
                    }
                    else
                    {
                        taxAmount += (grossAmount - item.FromAmountIncluding) * item.PercentAboveFrom;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void AddExplanation(Salary salary, TaxDefinitionItem item, decimal amount)
        {
            if (salary.TaxExplanation == null)
            {
                salary.TaxExplanation = new Dictionary<TaxDefinitionItem, decimal>();
            }

            salary.TaxExplanation.Add(item, amount);
            salary.TaxAmount += amount;
        }
    }
}
