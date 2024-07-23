using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerPhoneValidator :AbstractValidator<CustomerPhoneRequest>
{
    public CustomerPhoneValidator()
    {
        RuleFor(customerPhone => customerPhone.Phone).NotEmpty().WithMessage("Phone is required.");
        RuleFor(customerPhone => customerPhone.CountyCode).NotEmpty().WithMessage("County Code is required.");
    }
}