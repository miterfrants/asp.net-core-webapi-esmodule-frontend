using System;
using System.Net;

namespace Sample.Constants
{
    public enum ERROR_CODE
    {
        INVALID_EMAIL = 100001,
        PASSWORD_TOO_SHORT = 100002,
        PASSWORD_TOO_WEAK = 100003,

        // 2 business logic error 
        DUPLICATED_EMAIL = 200001,
        SIGNIN_FAILED = 200002,
        USER_NOT_FOUND = 200003,
        TOKEN_EXPIRED = 200004,
        UNAUTH_TOKEN = 200005,
        LOGIN_REQUIRE = 200006,

        // 3 internal error
        INTERNAL_ERROR = 5000001
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
        public ERROR_CODE errorCode;
        public CustomException(ERROR_CODE errorCode, HttpStatusCode code = HttpStatusCode.OK)
            : base()
        {
            this.code = code;
            this.errorCode = errorCode;
        }
    }
}