using FluentValidation;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Validators
{
    /// <summary>
    /// Validator for requests to create a new customer.
    /// </summary>
    public class NewCustomerValidator : AbstractValidator<NewCustomerRequest>
    {
        public NewCustomerValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage(ErrorMessages.ObjectRequired);
            
            RuleFor(x => x.Name)
                .SetValidator(new NameValidator<NewCustomerRequest>());
        }
    }
}