using System;
using System.Collections.Generic;

namespace Sample.Domain.Models
{
    public partial class UsersUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string ResetPasswordToken { get; set; }

        public DateTime? ResetExperation { get; set; }

        #endregion

    }
}
