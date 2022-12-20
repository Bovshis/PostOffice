using FluentValidation;

namespace UserService.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(30);
    }
}