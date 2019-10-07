using Sample.Constants;
public class ErrorHelper
{
    public static string GetErrorMessageByCode(ERROR_CODE code)
    {
        switch (code)
        {
            case ERROR_CODE.INVALID_EMAIL: return "Invalid Email";
            case ERROR_CODE.PASSWORD_TOO_SHORT: return "Password too short";
            case ERROR_CODE.PASSWORD_TOO_WEAK: return "Password too week";
            case ERROR_CODE.DUPLICATED_EMAIL: return "The email address you have entered is already registered";
            case ERROR_CODE.SIGNIN_FAILED: return "Email or password incorrect";
            case ERROR_CODE.INTERNAL_ERROR: return "Internal error";
            case ERROR_CODE.USER_NOT_FOUND: return "User not found";
            case ERROR_CODE.TOKEN_EXPIRED: return "Token expired";
            case ERROR_CODE.UNAUTH_TOKEN: return "Unauh token";
            default:
                return "Internal error";
        }
    }
}