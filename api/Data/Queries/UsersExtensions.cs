using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sample.Data.Queries
{
    public static partial class UsersExtensions
    {
        #region Generated Extensions
        public static IQueryable<Sample.Data.Entities.Users> ByEmail(this IQueryable<Sample.Data.Entities.Users> queryable, string email)
        {
            return queryable.Where(q => q.Email == email);
        }

        public static Sample.Data.Entities.Users GetByKey(this IQueryable<Sample.Data.Entities.Users> queryable, int id)
        {
            if (queryable is DbSet<Sample.Data.Entities.Users> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Sample.Data.Entities.Users> GetByKeyAsync(this IQueryable<Sample.Data.Entities.Users> queryable, int id)
        {
            if (queryable is DbSet<Sample.Data.Entities.Users> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Sample.Data.Entities.Users>(task);
        }

        public static IQueryable<Sample.Data.Entities.Users> ByResetPasswordToken(this IQueryable<Sample.Data.Entities.Users> queryable, string resetPasswordToken)
        {
            return queryable.Where(q => (q.ResetPasswordToken == resetPasswordToken || (resetPasswordToken == null && q.ResetPasswordToken == null)));
        }

        #endregion

    }
}
