using System;
using System.Collections.Generic;
using System.Net;

namespace GroceryStoreAPI.Models
{
    public class Result<T> where T : class
    {
        public T Data { get; }
        
        public string ErrorMessage { get; }
        
        public HttpStatusCode StatusCode { get; }

        public bool IsSuccess => Data != default(T);

        public Result(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public Result(string errorMessage, HttpStatusCode statusCode)
        {
            Data = default;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
        
    }
}