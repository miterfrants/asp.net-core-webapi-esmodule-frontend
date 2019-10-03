using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Constants
{
    public class ERROR
    {
        public static readonly ErrorObject INVALID_EMAIL = new ErrorObject(100001, "Invalid Email");
        public static readonly ErrorObject PASSWORD_TOO_SHORT = new ErrorObject(100002, "Password too short");
        public static readonly ErrorObject PASSWORD_TOO_WEAK = new ErrorObject(100003, "Password too week");
    }

    public class ErrorObject
    {
        public int ErrorCode;
        public string ErrorMessage;
        public ErrorObject(int ErrorCode,string ErrorMessage)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
        }
    }
}