using System;
using FluentValidation;
using Sample.Domain.Models;

namespace Sample.Domain.Validation
{
    public partial class UsersUpdateModelValidator
        : AbstractValidator<UsersUpdateModel>
    {
        public UsersUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Email).MaximumLength(64);
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password).MaximumLength(512);
            RuleFor(p => p.Salt).NotEmpty();
            RuleFor(p => p.Salt).MaximumLength(512);
            RuleFor(p => p.Username).MaximumLength(64);
            RuleFor(p => p.ResetPasswordToken).MaximumLength(64);
            #endregion
        }

    }
}
