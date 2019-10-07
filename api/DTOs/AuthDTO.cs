using System.ComponentModel.DataAnnotations;

namespace Sample.DTOs
{
    public class AuthSignupAndSinginDTO
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class ResetPasswordDTO : AuthSignupAndSinginDTO
    {
        [Required]
        public string ResetPasswordToken { get; set; }
    }

    public class ForgotPasswordDTO
    {
        [Required]
        public string redirectURL { get; set; }
        [Required]
        public string email { get; set; }
    }

}