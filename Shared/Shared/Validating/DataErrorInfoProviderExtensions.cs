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
        public static Func<bool> CannotBeEmpty(this string property)
        {
            return () => !string.IsNullOrEmpty(property);
        }

        public static bool CannotHaveLeadingWhiteSpaces(this string property)
        {
            return property.Equals(property.TrimStart());
        }

        public static bool CanOnlyContainLetters(this string property)
        {
            return
                string.IsNullOrEmpty(property) ||
                !Regex.IsMatch(property, @"(\W|\d)");
        }

        public static bool NeedsToStartWithCapitalLetter(this string property)
        {
            return
                string.IsNullOrEmpty(property) ||
                property.Substring(0, 1).Equals(property.Substring(0, 1).ToUpper());
        }

        public static IValidation MustHaveMinimumLengthOf(this string property, int length)
        {
            return new Validation("{0} must be at least " + length +" letters long!", () => property.Trim().Length >= length);
        }
    }
}
