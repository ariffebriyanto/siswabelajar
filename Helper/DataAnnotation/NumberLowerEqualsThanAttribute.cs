using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helper.DataAnnotation
{
    public class NumberLowerEqualsThanAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _comparisonProperty;

        public NumberLowerEqualsThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-dategreaterequalsthan", GetErrorMessage());
            MergeAttribute(context.Attributes, "data-val-dategreaterequalsthan-mindate", DateTime.MinValue.ToString("dd MMMM yyyy"));
            MergeAttribute(context.Attributes, "data-val-dategreaterequalsthan-maxdate", DateTime.MaxValue.ToString("dd MMMM yyyy"));
        }
        public string GetErrorMessage() => ErrorMessage;
        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (value == null) ? 0 : (decimal)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var comparisonValue = (property.GetValue(validationContext.ObjectInstance) == null) ? 0 : (decimal)property.GetValue(validationContext.ObjectInstance);

            if (currentValue > comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
