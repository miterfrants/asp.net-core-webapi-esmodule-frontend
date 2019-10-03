using Sample.Repositories;
using Sample.Constants;
using Microsoft.AspNetCore.Mvc;
// using Sample.Data.Entities;

namespace Sample.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [Route("forgot-password")]
        [HttpPost]
        public ActionResult<dynamic> ForgotPassword([FromBody] dynamic postBody)
        {
            string email = postBody.email.Value.ToString();
            string redirectUrl = postBody.redirectUrl.value.ToString();

            #region validation
            // TODO: postBody validation
            UsersRepository userRepo = new UsersRepository();
            // Users user = userRepo.GetUserByEmail(email);
            // if (user == null)
            {

            }
            #endregion

            // bool setStateResult = userRepo.SetupUserToForgotPasswordState(user);
            // if (setStateResult)
            // {
            // TODO: Implment SMTPService; 
            // SMTPService.Send($"{ENV.END_POINT}/auth/reset-password?token={user.reset_password_token}&redirect-url={HttpUtility.UrlEncode(redirectUrl);}";
            // }

            return new
            {
                data = CUSTOM_RESPONSE.STATUS.OK.ToString()
            };
        }

        // GET: Auth
        [Route("reset-password")]
        [HttpGet]
        public ActionResult<dynamic> ResetPassword([FromBody] dynamic postBody)
        {
            return "";
        }
    }
}