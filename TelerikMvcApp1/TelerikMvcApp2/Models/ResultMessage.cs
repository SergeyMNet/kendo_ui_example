using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp2.Models
{
    public class ResultMessage
    {
        public int ResultCode { get; set; }
        public string ErrorMessage { get; set; }
        public string CustomMessage { get; set; }

        public ResultMessage()
        {
            ResultCode = (int)EnumErrorCode.Ok;
            ErrorMessage = EnumErrorCode.Ok.ToString();
        }

        public ResultMessage(string message)
        {
            ResultCode = (int)EnumErrorCode.Ok;
            ErrorMessage = EnumErrorCode.Ok.ToString();
            CustomMessage = message;
        }

        public ResultMessage(EnumErrorCode errorCode)
        {
            ResultCode = (int)errorCode;
            ErrorMessage = errorCode.ToString();
        }

        public ResultMessage(EnumErrorCode errorCode, string message )
        {
            ResultCode = (int)errorCode;
            ErrorMessage = errorCode.ToString();
            CustomMessage = message;
        }
    }
}