using FluentValidation;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Validators
{
    public class CustomerRequestValidator : AbstractValidator<CustomerQueryRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<CustomerQueryRequest>());
        }
    }
}