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
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ErrorMessages.IdRequired)
                .GreaterThanOrEqualTo(1)
                .WithMessage(ErrorMessages.IdNotValid);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ErrorMessages.NameRequired)
                .MaximumLength(255)
                .WithMessage(ErrorMessages.NameLengthExceeded);
        }
    }
}