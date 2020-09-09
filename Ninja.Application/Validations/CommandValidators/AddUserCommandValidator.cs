using Ninja.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Ninja.Application.Common.EmailFormats;

namespace Ninja.Application.Validations
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(u => u.User.Name).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.User.Name).Length(2, 40).WithMessage(ValidationMessage.MinMaxLength);
            RuleFor(u => u.User.Email).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.User.Email).Matches(RegexFormat.GenericEmailRegex).WithMessage(ValidationMessage.EmailFormat);
            RuleFor(u => u.User.Lastname).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.User.Lastname).Length(2, 40).WithMessage(ValidationMessage.MinMaxLength);
            RuleFor(u => u.User.TelephoneNumber).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.User.TelephoneNumber).Matches(RegexFormat.GenericTelephoneNumberRegex).WithMessage(ValidationMessage.TelephoneNumberFormat);
            RuleFor(u => u.User.Age).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.User.Age).GreaterThanOrEqualTo(18).WithMessage(ValidationMessage.AgeRange);
            RuleFor(u => u.User.Address).NotNull().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.User.Address).Must(val => val.Count > 0).WithMessage(ValidationMessage.AtLeastOne);
            RuleForEach(u => u.User.Address).ChildRules(a => {
                a.RuleFor(d => d.Description).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
                a.RuleFor(d => d.Description).Length(2, 100).WithMessage(ValidationMessage.MinMaxLength);
            });
        }
    }
}
