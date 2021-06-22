using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CustomersQueryTests
    {
        [Fact]
        public async Task CustomersQuery_SuccessfulExecution_ReturnsOk()
        {
            var context = A.Fake<CustomerContext>();

            A.CallTo(() => context.AllCustomers()).Returns(new List<Customer>());

            var sut = new CustomersQuery(context);

            var result = await sut.Execute();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CustomersQuery_DatabaseException_ReturnsServiceUnavailable()
        {
            var context = A.Fake<CustomerContext>();
            A.CallTo(() => context.AllCustomers()).Throws<Exception>();

            var sut = new CustomersQuery(context);

            var result = await sut.Execute();
            result.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }
    }
}