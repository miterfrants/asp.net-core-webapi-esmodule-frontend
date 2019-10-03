using System;
using System.Net;

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
        public ErrorObject(int ErrorCode, string ErrorMessage)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
        }
    }

    public class CustomException : Exception
    {
        public HttpStatusCode code;
        public CustomException(string message, HttpStatusCode code = HttpStatusCode.OK)
            : base(message)
        {
            this.code = code;
        }
    }
}