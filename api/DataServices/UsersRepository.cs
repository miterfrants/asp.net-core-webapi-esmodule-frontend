using System;
using System.Linq;
using System.Collections.Generic;

using Sample.Data;
using Sample.Helpers;

namespace Sample.DataServices
{
    public class UsersDataService
    {
        public List<Users> GetList(DBContext dbContext)
        {
            return dbContext.Users.Where(x => 1 == 1).ToList();
        }

        public Users Register(string email, string password, DBContext dBContext)
        {
            SaltedPassword saltedPassword = CryptographicHelper.GenerateSaltedHash(64, password);
            Users newUser = new Users() { Email = email, Salt = saltedPassword.Salt, Password = saltedPassword.Hash, CreatedAt = DateTime.Now };
            try
            {
                dBContext.Users.Add(newUser);
                dBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return newUser;
        }

        public Boolean SetupUserToForgotPasswordState(Users user, DBContext dBContext)
        {
            // update user reset_password_token
            user.ResetPasswordToken = CryptographicHelper.GetSpecificLengthRandomString(64, true);
            user.ResetExperation = DateTime.Now.AddMinutes(10);
            return dBContext.SaveChanges() == 1;
        }

        public Users GetUserByEmail(string email, DBContext dbContext)
        {
            Users user = dbContext.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }

    }
}