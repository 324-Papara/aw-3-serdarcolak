using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerDetailValidator:AbstractValidator<CustomerDetailRequest>
{
    public CustomerDetailValidator()
    {
        RuleFor(customerDetail => customerDetail.FatherName).NotEmpty().WithMessage("Father Name is required.");
        RuleFor(customerDetail => customerDetail.MotherName).NotEmpty().WithMessage("Mother Name is required.");
        RuleFor(customerDetail => customerDetail.Occupation).NotEmpty().WithMessage("Occupation Line is required.");
        RuleFor(customerDetail => customerDetail.EducationStatus).NotEmpty().WithMessage("Education Status Line is required.");
        RuleFor(customerDetail => customerDetail.MontlyIncome).NotEmpty().WithMessage("Montly Income Line is required.");
    }
}