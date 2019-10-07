using System;
using System.Linq;
using System.Collections.Generic;

using Sample.DTOs;
using Sample.Data;
using Sample.Helpers;

namespace Sample.DataServices
{
    public class UsersDataService
    {
        public static List<Users> GetList(DBContext dbContext)
        {
            return dbContext.Users.Where(x => 1 == 1).ToList();
        }

        public static Users Signup(string email, string password, DBContext dBContext)
        {
            string salt = CryptographicHelper.GetSalt(64);
            string hash = CryptographicHelper.GenerateSaltedHash(salt, password);
            Users newUser = new Users() { Email = email, Salt = salt, Password = hash, CreatedAt = DateTime.Now };
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

        public static Boolean SetUserToForgotPasswordState(Users user, DBContext dBContext)
        {
            // update user reset_password_token
            user.ResetPasswordToken = CryptographicHelper.GetSpecificLengthRandomString(64, true);
            user.ResetExperation = DateTime.Now.AddMinutes(10);
            return dBContext.SaveChanges() == 1;
        }

        public static Users GetUserByEmail(string email, DBContext dbContext)
        {
            Users user = dbContext.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }

        public static Users GetUserByHash(string email, string password, DBContext dbContext)
        {
            Users user = dbContext.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            if (user.Password != CryptographicHelper.GenerateSaltedHash(user.Salt, password))
            {
                return null;
            }
            return user;
        }

        public static bool ResetPassword(Users user, string password, DBContext dbContext)
        {
            string salt = CryptographicHelper.GetSalt(64);
            string hash = CryptographicHelper.GenerateSaltedHash(password, salt);
            user.Password = hash;
            user.Salt = salt;
            user.ResetExperation = null;
            user.ResetPasswordToken = null;
            return dbContext.SaveChanges() == 1;
        }
    }
}