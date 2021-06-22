using FluentValidation;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Validators
{
    /// <summary>
    /// Validator for requests to modify an existing customer or test data read from the JSON file.
    /// </summary>
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage(ErrorMessages.ObjectRequired);
            
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<UpdateCustomerRequest>());

            RuleFor(x => x.Name)
                .SetValidator(new NameValidator<UpdateCustomerRequest>());
        }
    }
}