using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Shared.Interfaces;

namespace Shared.Validating
{
    public static class DataErrorInfoProviderExtensions
    {
        public static IValidation CannotBeEmpty(this string property)
        {
            return new Validation("{0} cannot be empty", 
                !string.IsNullOrEmpty(property));
        }

        public static IValidation CannotHaveLeadingWhiteSpaces(this string property)
        {
            return new Validation("{0} cannot have leading whitespaces", 
                property.Equals(property.TrimStart()));
        }

        public static IValidation CanOnlyContainLetters(this string property)
        {
            return new Validation("{0} can only contain letters",
                string.IsNullOrEmpty(property) ||
                !Regex.IsMatch(property, @"(\W|\d)"));
        }

        public static IValidation NeedsToStartWithCapitalLetter(this string property)
        {
            return new Validation("{0} needs to start with a capital letter",
                string.IsNullOrEmpty(property) ||
                property.Substring(0, 1).Equals(property.Substring(0, 1).ToUpper()));
        }

        public static IValidation MustHaveMinimumLengthOf(this string property, int length)
        {
            return new Validation("{0} must be at least " + length +" letters long", 
                property.Trim().Length >= length);
        }

        public static IValidation MustBeGreaterOrEqualTo<T>(this T property, T minValue) where T:  IComparable
        {
            return new Validation("{0} must be greater or equal to " + minValue.ToString(),
                property.CompareTo(minValue) >= 0);
        }

        public static IValidation MustBeGreaterThan<T>(this T property, T minValue) where T : IComparable
        {
            return new Validation("{0} must be greater than " + minValue.ToString(),
                property.CompareTo(minValue) > 0);
        }

        public static IValidation MustBeSmallerThan<T>(this T property, T minValue) where T : IComparable
        {
            return new Validation("{0} must be smaller than " + minValue.ToString(),
                property.CompareTo(minValue) < 0);
        }

        public static IValidation MustBeSmallerOrEqualTo<T>(this T property, T minValue) where T : IComparable
        {
            return new Validation("{0} must be smaller or equal to " + minValue.ToString(),
                property.CompareTo(minValue) <= 0);
        }
    }
}
