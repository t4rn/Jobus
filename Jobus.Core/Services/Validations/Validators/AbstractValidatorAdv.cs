using FluentValidation;
using Jobus.Common.Extensions;
using System;

namespace Jobus.Core.Services.Validations.Validators
{
    public class AbstractValidatorAdv<T> : AbstractValidator<T>
    {
        protected static string DefaultMessage = "Incorrect {PropertyName} '{PropertyValue}'.";
        protected static string DefaultLengthMessage = "Incorrect '{PropertyName}' length equal '{TotalLength}' - must be between '{MinLength}' and '{MaxLength}' chars.";
        protected static string DefaultLengthMessageExact = "Incorrect '{PropertyName}' length equal '{TotalLength}' - must be exactly '{MaxLength}' chars.";
        protected static string DefaultGreaterThanMessage = "{PropertyName} '{PropertyValue}' must be greater than '{ComparisonValue}'.";
        protected static string DefaultLessThanMessage = "{PropertyName} '{PropertyValue}' must be less than '{ComparisonValue}'.";
        protected static string DefaultNotEmptyMessage = "Empty {PropertyName}.";

        public AbstractValidatorAdv()
        {
        }

        protected bool BeOneOfEnumType(string code, Type enumType)
        {
            return Enum.IsDefined(enumType, code);
        }

        protected bool HasCertainLength(int? number, int from, int to)
        {
            return HasCertainLength(number.ToString(), from, to);
        }

        protected bool HasCertainLength(double? number, int from, int to)
        {
            return HasCertainLength(number.ToString(), from, to);
        }

        private bool HasCertainLength(string number, int from, int to)
        {
            //Regex regex = new Regex($@"^\d{{{from},{to}}}$");
            //bool isValid = regex.IsMatch(number);
            bool isValid = number.HasCertainLength(from, to);

            return isValid;
        }

        protected string DefaultInRangeMessage(int from, int to)
        {
            return $"{{PropertyName}} must be between {from} and {to} digits.";
        }
    }
}
