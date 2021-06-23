using FluentValidation.TestHelper;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Validators;
using Xunit;

namespace GroceryStoreAPI.Tests.ValidatorTests
{
    public class NewCustomerValidatorTests
    {
        private readonly NewCustomerValidator _validator;

        public NewCustomerValidatorTests()
        {
            _validator = new NewCustomerValidator();
        }
        
        [Fact]
        public void NewCustomerValidator_NullName_ProducesError()
        {
            var sut = new NewCustomerRequest
            {
                Name = null
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void NewCustomerValidator_EmptyName_ProducesError()
        {
            var sut = new NewCustomerRequest
            {
                Name = string.Empty
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void NewCustomerValidator_LongName_ProducesError()
        {
            var lorem = new Bogus.DataSets.Lorem();
            
            var sut = new NewCustomerRequest
            {
                Name = lorem.Paragraph(10)
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void NewCustomerValidator_ValidName_PassesValidation()
        {
            var name = new Bogus.DataSets.Name();
            
            var sut = new NewCustomerRequest
            {
                Name = name.FullName()
            };

            var result = _validator.TestValidate(sut);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}