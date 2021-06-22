using FluentValidation;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Validators
{
    public class CustomerQueryRequestValidator : AbstractValidator<CustomerQueryRequest>
    {
        public CustomerQueryRequestValidator()
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<CustomerQueryRequest>());
        }
    }
}