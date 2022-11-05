using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class BaseResponse
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object Data { get; set; }
    }

    public class APIResponse : BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public int IntStatusCode
        {
            get
            {
                return (int)StatusCode;
            }
        }
        public static BaseResponse GetResponse(APIResponse response)
        {
            return new BaseResponse
            {
                IsValid = response.IsValid,
                Data = response.Data,
                Errors = response.Errors,
            };
        }
        public static APIResponse GetErrorResponseFromValidation(ValidationResult validationResult)
        {
            return new APIResponse
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors,
                StatusCode = validationResult.StatusCode
            };
        }
        public static APIResponse GetExceptionResponse(Exception ex)
        {
            return new APIResponse
            {
                IsValid = false,
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Errors = new List<string>()
                    {
                        $"Exception Message : {ex.Message} {Environment.NewLine} Inner Excption Message :  {ex.InnerException?.Message}"
                    }
            };
        }
    }
}
