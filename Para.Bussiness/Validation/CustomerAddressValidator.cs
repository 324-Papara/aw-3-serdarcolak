using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerAddressValidator :AbstractValidator<CustomerAddressRequest>
{
    public CustomerAddressValidator()
    {
        RuleFor(customerAddress => customerAddress.Country).NotEmpty().WithMessage("Country is required.");
        RuleFor(customerAddress => customerAddress.City).NotEmpty().WithMessage("City is required.");
        RuleFor(customerAddress => customerAddress.AddressLine).NotEmpty().WithMessage("Address Line is required.");
    }
    
}