using FluentValidation;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Validators
{
    public class CustomerQueryRequestValidator : AbstractValidator<CustomerQueryRequest>
    {
        public CustomerQueryRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ErrorMessages.IdRequired)
                .GreaterThanOrEqualTo(1)
                .WithMessage(ErrorMessages.IdNotValid);
        }
    }
}