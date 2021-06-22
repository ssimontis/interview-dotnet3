using FluentValidation.TestHelper;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Validators;
using Xunit;

namespace GroceryStoreAPI.Tests.ValidatorTests
{
    public class UpdateCustomerValidatorTests
    {
        private readonly UpdateCustomerValidator _validator;

        public UpdateCustomerValidatorTests()
        {
            _validator = new UpdateCustomerValidator();
        }

        [Fact]
        public void UpdateCustomerValidator_EmptyId_ProducesError()
        {
            var sut = new UpdateCustomerRequest
            {
                Id = 0
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        
        [Fact]
        public void UpdateCustomerValidator_NegativeId_ProducesError()
        {
            var sut = new UpdateCustomerRequest
            {
                Id = -1
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        
        [Fact]
        public void UpdateCustomerValidator_ValidId_PassesValidation()
        {
            var sut = new UpdateCustomerRequest
            {
                Id = 1
            };

            var result = _validator.TestValidate(sut);

            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void UpdateCustomerValidator_NullName_ProducesError()
        {
            var sut = new UpdateCustomerRequest
            {
                Name = null
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void UpdateCustomerValidator_EmptyName_ProducesError()
        {
            var sut = new UpdateCustomerRequest
            {
                Name = string.Empty
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void UpdateCustomerValidator_LongName_ProducesError()
        {
            var lorem = new Bogus.DataSets.Lorem();
            
            var sut = new UpdateCustomerRequest
            {
                Name = lorem.Paragraph(10)
            };

            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
        [Fact]
        public void UpdateCustomerValidator_ValidName_PassesValidation()
        {
            var name = new Bogus.DataSets.Name();
            
            var sut = new UpdateCustomerRequest
            {
                Name = name.FullName()
            };

            var result = _validator.TestValidate(sut);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}