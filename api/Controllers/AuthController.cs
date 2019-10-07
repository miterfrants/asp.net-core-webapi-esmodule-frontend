using System;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Sample.Constants;
using Sample.Data;
using Sample.DTOs;
using Sample.DataServices;
using Sample.Helpers;

namespace Sample.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {

        private readonly DBContext _dbContext;
        private readonly string _jwtKey;
        public AuthController(DBContext dbContext, IOptions<AppSettings> appSettings)
        {
            _jwtKey = appSettings.Value.Secrets.JwtKey;
            _dbContext = dbContext;
        }

        [Route("signin")]
        [HttpPost]
        public ActionResult<dynamic> Singin([FromBody] AuthSignupAndSinginDTO postBody)
        {
            #region verification
            Regex rgx = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            if (!rgx.IsMatch(postBody.email))
            {
                throw new CustomException(ERROR_CODE.INVALID_EMAIL, HttpStatusCode.ExpectationFailed);
            }
            Users user = UsersDataService.GetUserByHash(postBody.email, postBody.password, _dbContext);
            if (user == null)
            {
                throw new CustomException(ERROR_CODE.SIGNIN_FAILED, HttpStatusCode.Unauthorized);
            }
            #endregion

            #region generate token
            // todo: implment role
            string token = JWTHelper.GenerateToken(_jwtKey, 6, new { userId = user.Id, role = "", username = user.Username });
            return new { token = token };
            #endregion
        }

        [Route("signup")]
        [HttpPost]
        public ActionResult<dynamic> Signup([FromBody] AuthSignupAndSinginDTO postBody)
        {
            #region verification

            Regex rgx = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            if (!rgx.IsMatch(postBody.email))
            {
                throw new CustomException(ERROR_CODE.INVALID_EMAIL, HttpStatusCode.ExpectationFailed);
            }
            Users user = UsersDataService.GetUserByEmail(postBody.email, _dbContext);
            if (user != null)
            {
                throw new CustomException(ERROR_CODE.DUPLICATED_EMAIL, HttpStatusCode.ExpectationFailed);
            }

            #endregion

            Users signupUser = null;
            signupUser = UsersDataService.Signup(postBody.email, postBody.password, _dbContext);
            return new { email = signupUser.Email, createdAt = signupUser.CreatedAt, id = signupUser.Id };
        }

        [Route("forgot-password")]
        [HttpPost]
        public ActionResult<dynamic> ForgotPassword([FromBody] ForgotPasswordDTO postBody)
        {
            #region verification
            Users user = UsersDataService.GetUserByEmail(postBody.email, _dbContext);
            if (user == null)
            {
                throw new CustomException(ERROR_CODE.USER_NOT_FOUND, HttpStatusCode.NotFound);
            }
            #endregion

            bool setStateResult = UsersDataService.SetUserToForgotPasswordState(user, _dbContext);
            if (!setStateResult)
            {
                throw new CustomException(ERROR_CODE.INTERNAL_ERROR, HttpStatusCode.InternalServerError);
            }

            // todo: implement smtp snder

            return new { result = CUSTOM_RESPONSE.STATUS.OK.ToString() };
        }

        [Route("reset-password")]
        [HttpPost]
        public ActionResult<dynamic> ResetPassword([FromBody] ResetPasswordDTO postBody)
        {
            #region verification
            Users user = UsersDataService.GetUserByEmail(postBody.email, _dbContext);
            if (user.ResetPasswordToken != postBody.ResetPasswordToken || user.ResetExperation <= DateTime.Now)
            {
                throw new CustomException(ERROR_CODE.TOKEN_EXPIRED, HttpStatusCode.Unauthorized);
            }
            #endregion

            bool result = UsersDataService.ResetPassword(user, postBody.password, _dbContext);
            if (!result)
            {
                throw new CustomException(ERROR_CODE.INTERNAL_ERROR, HttpStatusCode.InternalServerError);
            }
            return new { result = CUSTOM_RESPONSE.STATUS.OK.ToString() };
        }

        [Route("refresh-token")]
        [HttpPost]
        public ActionResult<dynamic> RefreshToken()
        {
            string authorization = Request.Headers["Authorization"];
            string token = authorization.Substring("Bearer ".Length).Trim();
            dynamic extraPayload = JWTHelper.GetExtraPayload(_jwtKey, token);
            if (extraPayload == null)
            {
                throw new CustomException(ERROR_CODE.UNAUTH_TOKEN, HttpStatusCode.Unauthorized);
            }
            string newToken = JWTHelper.GenerateToken(_jwtKey, 6, extraPayload);
            return new { token = newToken };
        }
    }
}