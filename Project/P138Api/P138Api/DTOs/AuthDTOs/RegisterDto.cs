using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace P138Api.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto> 
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Required");

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("InCorrect Email");
        }

    }
}
