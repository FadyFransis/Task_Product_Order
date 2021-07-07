using App.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace App.API.Helper
{
    public static class HelperClass<T> where T : class
    {
        public static ResponseModel<T> CreateResponseModel(T result, bool isError, string errorMessage)
        {
            if (isError)
            {
                ResponseModel<T> res = new ResponseModel<T>()
                {
                    Timestamp = DateTime.Now,
                    IsError = true,
                    Result = null,
                    Description = errorMessage
                };
                return res;
            }
            else
            {
                ResponseModel<T> res = new ResponseModel<T>()
                {
                    Timestamp = DateTime.Now,
                    IsError = false,
                    Result = result,
                };
                return res;
            }


        }

       
    }
}
