using FluentValidation;

namespace UserService.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(30);
    }
}