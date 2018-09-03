using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto.Error
{
    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "errorcode")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errormessage")]
        public string ErrorMessage { get; set; }

        public static ErrorResponse FromCode(ErrorCode code)
        {
            return new ErrorResponse
            {
                ErrorCode = (int)code,
                ErrorMessage = code.ErrorMessage()
            };
        }

        public static ErrorResponse FromCode(ErrorCode code, string message)
        {
            return new ErrorResponse
            {
                ErrorCode = (int)code,
                ErrorMessage = code.ErrorMessage() + " " + message
            };
        }

        public static ErrorResponse FromCode(ErrorCode code, object[] values)
        {
            return new ErrorResponse
            {
                ErrorCode = (int)code,
                ErrorMessage = string.Format(code.ErrorMessage(), values)
            };
        }

    }
}
