using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sample.Data.Mapping
{
    public partial class UsersMap
        : IEntityTypeConfiguration<Sample.Data.Entities.Users>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Sample.Data.Entities.Users> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("users", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("nvarchar(64)")
                .HasMaxLength(64);

            builder.Property(t => t.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("nvarchar(512)")
                .HasMaxLength(512);

            builder.Property(t => t.Salt)
                .IsRequired()
                .HasColumnName("salt")
                .HasColumnType("nvarchar(512)")
                .HasMaxLength(512);

            builder.Property(t => t.Username)
                .HasColumnName("username")
                .HasColumnType("nvarchar(64)")
                .HasMaxLength(64);

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(t => t.DeletedAt)
                .HasColumnName("deleted_at")
                .HasColumnType("datetime");

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime");

            builder.Property(t => t.ResetPasswordToken)
                .HasColumnName("reset_password_token")
                .HasColumnType("nvarchar(64)")
                .HasMaxLength(64);

            builder.Property(t => t.ResetExperation)
                .HasColumnName("reset_experation")
                .HasColumnType("datetime");

            // relationships
            #endregion
        }

    }
}
