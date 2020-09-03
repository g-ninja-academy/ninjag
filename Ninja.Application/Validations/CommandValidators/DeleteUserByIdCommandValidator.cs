using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Users.Commands;

namespace Ninja.Application.Validations
{
    class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommand>
    {
        public readonly IUnitOfWork _unitOfWork;
        public DeleteUserByIdCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(u => u.Id).NotEmpty().WithMessage(ValidationMessage.EmptyMessage);
            RuleFor(u => u.Id).MustAsync(UserAlreadyExist).WithMessage(ValidationMessage.UserNotFounded);
        }

        private async Task<bool> UserAlreadyExist(Guid Id, CancellationToken cancellationToken) 
        {
            var user = await _unitOfWork.Users.FindSingle(x => x.Id.Equals(Id));

            return user != null ? true : false; 
        }

    }
}
