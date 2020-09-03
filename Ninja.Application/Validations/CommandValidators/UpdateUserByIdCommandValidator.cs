using FluentValidation;
using Ninja.Application.Common.EmailFormats;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Application.Validations
{
    class UpdateUserByIdCommandValidator : AbstractValidator<UpdateUserByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserByIdCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //Check if user exists
            RuleFor(u => u.Id).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.Id).MustAsync(UserAlreadyExist).WithMessage(ValidationMessage.UserNotFounded);

            //Check new User info
            RuleFor(u => u.User).NotNull().WithMessage(ValidationMessage.EmptyMessage);
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

        private async Task<bool> UserAlreadyExist(Guid Id, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.FindSingle(x => x.Id.Equals(Id));

            return user != null ? true : false;
        }
    }
}
