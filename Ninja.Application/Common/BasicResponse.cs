using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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

        [JsonIgnore] public bool Success { get; set; }
    }
}