using FluentValidation.TestHelper;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Validators;
using Xunit;

namespace GroceryStoreAPI.Tests.ValidatorTests
{
    public class CustomerRequestValidatorTests
    {
        private readonly CustomerRequestValidator _validator;

        public CustomerRequestValidatorTests()
        {
            _validator = new CustomerRequestValidator();
        }
        
        [Fact]
        public void CustomerRequestValidator_EmptyId_ProducesError()
        {
            var sut = new CustomerQueryRequest
            {
                Id = default
            };

            _validator.TestValidate(sut).ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void CustomerRequestValidator_InvalidId_ProducesError()
        {
            var sut = new CustomerQueryRequest
            {
                Id = -1
            };

            _validator.TestValidate(sut).ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void CustomerRequestValidator_ValidId_ProducesNoError()
        {
            var sut = new CustomerQueryRequest
            {
                Id = 1
            };
            
            _validator.TestValidate(sut).ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}