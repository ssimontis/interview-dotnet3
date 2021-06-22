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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Xunit;

namespace GroceryStoreAPI.Tests.CommandTests
{
    public class NewCustomerCommandTests
    {
        [Fact]
        public async Task NewCustomerCommand_InvalidRequest_ReturnsBadRequest()
        {
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<NewCustomerRequest>>();
            var addRequest = A.Fake<NewCustomerRequest>();
            var validationResult = A.Fake<ValidationResult>();

            A.CallTo(() => validationResult.IsValid)
                .Returns(false);
            A.CallTo(() => validator.ValidateAsync(addRequest, default))
                .Returns(validationResult);

            var sut = new NewCustomerCommand(validator, context);

            var result = await sut.Execute(addRequest);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task NewCustomerCommand_DatabaseException_ReturnsServiceUnavailable()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<NewCustomerRequest>>();
            var addRequest = A.Fake<NewCustomerRequest>();
            var validationResult = A.Fake<ValidationResult>();
            var customer = A.Fake<Customer>();

            A.CallTo(() => context.Customers)
                .Returns(fakeDbSet);
            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(addRequest, default))
                .Returns(validationResult);
            A.CallTo(() => fakeDbSet.Add(customer))
                .Throws<Exception>();

            var sut = new NewCustomerCommand(validator, context);

            var result = await sut.Execute(addRequest);
            result.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }
    }
}