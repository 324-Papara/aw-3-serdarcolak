using FluentValidation;
using Para.Bussiness.Command;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    
    public CustomerValidator()
    {
        RuleFor(customer => customer.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(customer => customer.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(customer => customer.IdentityNumber)
            .NotEmpty().WithMessage("Identity number is required.")
            .Length(11).WithMessage("Identity number must be 11 characters long.");

        RuleFor(customer => customer.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(customer => customer.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAValidAge).WithMessage("Customer must be at least 18 years old.");
    }

    private bool BeAValidAge(DateTime dateOfBirth)
    {
        int currentYear = DateTime.Now.Year;
        int birthYear = dateOfBirth.Year;
        return (currentYear - birthYear) >= 18;
    }
    
}