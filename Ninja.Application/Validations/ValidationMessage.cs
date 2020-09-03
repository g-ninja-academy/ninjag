using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Validations
{
    class ValidationMessage
    {
        public const string EmptyMessage = "{PropertyName} can not be empty.";
        public const string MinMaxLength = "{PropertyName} must have a minimum length of {MinLength} and a maximum length of {MaxLength}.";
        public const string EmailFormat = "{PropertyName} is not a valid email format.";
        public const string UserNotFounded = "We can't find the specified user: {PropertyValue}.";
        public const string TelephoneNumberFormat = "{PropertyName} is not a valid telephone format.";
        public const string AgeRange = "{PropertyName} must be between age range {ComparisonValue}";
    }
}
