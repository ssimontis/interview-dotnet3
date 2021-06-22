using FluentValidation;
using FluentValidation.Validators;

namespace GroceryStoreAPI.Validators
{
    public class IdValidator<T> : PropertyValidator<T, long>
    {
        public override bool IsValid(ValidationContext<T> context, long value)
        {
            switch (value)
            {
                case 0:
                    context.AddFailure(ErrorMessages.IdRequired);

                    return false;
                case <= 0:
                    context.AddFailure(ErrorMessages.IdNotValid);

                    return false;
                default:
                    return true;
            }
        }

        public override string Name => "IdValidator";
    }
}