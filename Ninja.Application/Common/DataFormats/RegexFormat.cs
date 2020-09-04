using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common.EmailFormats
{
    class RegexFormat
    {
        public const string GenericEmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string GenericTelephoneNumberRegex = @"(\(\d{3}\)[.-]?|\d{3}[.-]?)?\d{3}[.-]?\d{4}";
    }
}
