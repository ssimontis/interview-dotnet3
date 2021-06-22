using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Queries;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GroceryStoreAPI.Tests.QueryTests
{
    public class CustomerQueryTests
    {
        [Fact]
        public async Task CustomerQuery_InvalidObject_ReturnsBadRequest()
        {
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<CustomerQueryRequest>>();
            var customerRequest = A.Fake<CustomerQueryRequest>();
            var validationResult = A.Fake<ValidationResult>();
            
            A.CallTo(() => validationResult.IsValid)
                .Returns(false);
            A.CallTo(() => validator.ValidateAsync(customerRequest, default))
                .Returns(validationResult);

            var sut = new CustomerQuery(validator, context);

            var result = await sut.Execute(customerRequest);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CustomerQuery_ValidObjectNotInDatabase_ReturnsNotFound()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<CustomerQueryRequest>>();
            var customerRequest = A.Fake<CustomerQueryRequest>();
            var validationResult = A.Fake<ValidationResult>();

            A.CallTo(() => context.Customers).Returns(fakeDbSet);
            A.CallTo(() => fakeDbSet.FindAsync()).Returns(null);

            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(customerRequest, default))
                .Returns(validationResult);

            var sut = new CustomerQuery(validator, context);

            var result = await sut.Execute(customerRequest);
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CustomerQuery_ValidObjectFoundInDatabase_ReturnsOk()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<CustomerQueryRequest>>();
            var customerRequest = A.Fake<CustomerQueryRequest>();
            var validationResult = A.Fake<ValidationResult>();
            var fakeCustomer = A.Fake<Customer>();
            
            A.CallTo(() => context.Customers).Returns(fakeDbSet);
            A.CallTo(() => fakeDbSet.FindAsync()).Returns(fakeCustomer);

            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(customerRequest, default))
                .Returns(validationResult);

            var sut = new CustomerQuery(validator, context);

            var result = await sut.Execute(customerRequest);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task CustomerQuery_DatabaseException_ReturnsServiceUnavailable()
        {
            var fakeDbSet = A.Fake<DbSet<Customer>>();
            var context = A.Fake<CustomerContext>();
            var validator = A.Fake<IValidator<CustomerQueryRequest>>();
            var customerRequest = A.Fake<CustomerQueryRequest>();
            var validationResult = A.Fake<ValidationResult>();

            A.CallTo(() => context.Customers).Returns(fakeDbSet);
            A.CallTo(() => fakeDbSet.FindAsync()).Throws<Exception>();

            A.CallTo(() => validationResult.IsValid)
                .Returns(true);
            A.CallTo(() => validator.ValidateAsync(customerRequest, default))
                .Returns(validationResult);

            var sut = new CustomerQuery(validator, context);

            var result = await sut.Execute(customerRequest);
            result.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }
    }
}