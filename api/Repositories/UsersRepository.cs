using System;
using System.Linq;
using System.Collections.Generic;
using Sample.Data.Entities;
using Sample.Data;
using Sample.Helpers;
using Newtonsoft.Json;

namespace Sample.Repositories
{
    public class UsersRepository
    {

        public Users Register(string email, string password, SampleContext DBContext = null)
        {
            SaltedPassword saltedPassword = CryptographicHelper.GenerateSaltedHash(64, password);
            Users newUser = new Users() { Email = email, Salt = saltedPassword.Salt, Password = saltedPassword.Hash, CreatedAt = DateTime.Now };
            try
            {
                DBContext.Users.Add(newUser);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return newUser;
        }

        public Boolean SetupUserToForgotPasswordState(Users user, SampleContext DBContext = null)
        {
            // update user reset_password_token
            user.ResetPasswordToken = CryptographicHelper.GetSpecificLengthRandomString(64, true);
            user.ResetExperation = DateTime.Now.AddMinutes(10);
            return DBContext.SaveChanges() == 1;
        }

        public Users GetUserByEmail(string email, SampleContext DBContext = null)
        {
            Users user = DBContext.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }
    }
}