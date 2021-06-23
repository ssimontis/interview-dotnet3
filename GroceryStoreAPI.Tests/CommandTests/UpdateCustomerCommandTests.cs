using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GroceryStoreAPI.Commands;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GroceryStoreAPI.Tests.CommandTests
{
    public class UpdateCustomerCommandTests
    {
        [Fact]
        public async Task UpdateCustomerCommand_InvalidRequest_ReturnsBadRequest()
        {
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<UpdateCustomerRequest>>();
            var updateRequest = A.Fake<UpdateCustomerRequest>();
            var validationResult = A.Fake<ValidationResult>();

            A.CallTo(() => validationResult.IsValid)
                .Returns(false);
            A.CallTo(() => validator.ValidateAsync(updateRequest, default))
                .Returns(validationResult);

            var sut = new UpdateCustomerCommand(validator, context);

            var result = await sut.Execute(updateRequest);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateCustomerCommand_DatabaseException_ReturnsServiceUnavailable()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<UpdateCustomerRequest>>();
            var updateRequest = A.Fake<UpdateCustomerRequest>();
            var validationResult = A.Fake<ValidationResult>();

            A.CallTo(() => context.Customers).Returns(fakeDbSet);
            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(updateRequest, default))
                .Returns(validationResult);
            A.CallTo(() => fakeDbSet.FindAsync(updateRequest.Id))
                .Throws<Exception>();

            var sut = new UpdateCustomerCommand(validator, context);

            var result = await sut.Execute(updateRequest);
            result.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }
        
        [Fact]
        public async Task UpdateCustomerCommand_MissingOriginalObject_ReturnsNotFound()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<UpdateCustomerRequest>>();
            var updateRequest = A.Fake<UpdateCustomerRequest>();
            var validationResult = A.Fake<ValidationResult>();

            A.CallTo(() => context.Customers).Returns(fakeDbSet);
            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(updateRequest, default))
                .Returns(validationResult);
            A.CallTo(() => fakeDbSet.FindAsync(updateRequest.Id))
                .Returns(null);

            var sut = new UpdateCustomerCommand(validator, context);

            var result = await sut.Execute(updateRequest);
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCustomerCommand_ValidObjectFound_ReturnsOk()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<UpdateCustomerRequest>>();
            var updateRequest = A.Fake<UpdateCustomerRequest>();
            var validationResult = A.Fake<ValidationResult>();
            var customer = A.Fake<Customer>();
            
            A.CallTo(() => context.Customers).Returns(fakeDbSet);
            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(updateRequest, default))
                .Returns(validationResult);
            A.CallTo(() => fakeDbSet.FindAsync(updateRequest.Id))
                .Returns(customer);

            var sut = new UpdateCustomerCommand(validator, context);

            var result = await sut.Execute(updateRequest);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}