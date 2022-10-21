﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validator for validating that the specific <see cref="IList"/> is not null and has exactly one item.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ListOneItemAndRequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validate that the specific <see cref="IList"/> is not null and has exactly one item.
        /// </summary>
        /// <param name="value">Value to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if valid.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("List must contain 1 item. Actual: NULL.");

            if (value is IList list)
            {
                if (list.Count != 1)
                    return new ValidationResult($"List must contain exactly one item. Actual: {list.Count}.");

                return ValidationResult.Success;
            }

            return new ValidationResult("Unknown type.");
        }
    }
}