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
            RuleFor(u => u.UserViewModel.Name).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.UserViewModel.Name).Length(2, 40).WithMessage(ValidationMessage.MinMaxLength);
            RuleFor(u => u.UserViewModel.Email).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.UserViewModel.Email).Matches(RegexFormat.GenericEmailRegex).WithMessage(ValidationMessage.EmailFormat);
            RuleFor(u => u.UserViewModel.Lastname).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.UserViewModel.Lastname).Length(2, 40).WithMessage(ValidationMessage.MinMaxLength);
            RuleFor(u => u.UserViewModel.TelephoneNumber).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.UserViewModel.TelephoneNumber).Matches(RegexFormat.GenericTelephoneNumberRegex).WithMessage(ValidationMessage.TelephoneNumberFormat);
            RuleFor(u => u.UserViewModel.Age).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.UserViewModel.Age).InclusiveBetween(18,60).WithMessage(ValidationMessage.AgeRange);
            RuleFor(u => u.UserViewModel.Address).NotNull().WithMessage("");
            RuleForEach(u => u.UserViewModel.Address).ChildRules(a => {
                a.RuleFor(d => d.Description).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
                a.RuleFor(d => d.Description).Length(2, 100).WithMessage(ValidationMessage.MinMaxLength);
            });
        }
    }
}
