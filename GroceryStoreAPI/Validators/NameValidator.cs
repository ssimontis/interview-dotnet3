using FluentValidation;
using FluentValidation.Validators;

namespace GroceryStoreAPI.Validators
{
    public class NameValidator<T> : PropertyValidator<T, string>
    {
        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                context.AddFailure(ErrorMessages.NameRequired);

                return false;
            }

            if (value.Length > 255)
            {
                context.AddFailure(ErrorMessages.NameLengthExceeded);

                return false;
            }

            return true;
        }

        public override string Name => "NameValidator";
    }
}