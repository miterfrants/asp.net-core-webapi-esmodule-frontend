using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sample.Data
{
    public partial class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<Sample.Data.Entities.Users> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Sample.Data.Mapping.UsersMap());
            #endregion
        }
    }
}
