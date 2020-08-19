using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common
{
    public class BasicResponse
    {
        public BasicResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }
        public string Message { get; set; }

        public bool Success { get; set; }
    }
}
