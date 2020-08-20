﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Application.Common
{
    public static class Response
    {
        public static Response<T> Ok200<T>(T data)
        {
            return new Response<T>(data, "Success",200, true);
        }
    }


    public class Response<T> : BasicResponse
    {
        public Response(T data, string message, int statusCode, bool success) : base(message, success)
        {
            Data = data;
            StatusCode = statusCode;
        }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }
}