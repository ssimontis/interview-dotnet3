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
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ErrorMessages.NameRequired)
                .MaximumLength(255)
                .WithMessage(ErrorMessages.NameLengthExceeded);
        }
    }
}