using System.Net;
using FluentAssertions;
using GroceryStoreAPI.Models;
using Xunit;

namespace GroceryStoreAPI.Tests.ModelTests
{
    public class ResultTests
    {
        [Fact]
        public void Result_ValidDataPayload_ReturnsSuccess()
        {
            var customer = new Customer();
            var sut = new Result<Customer>(customer);

            sut.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Result_ErrorPayload_ReturnsFailure()
        {
            var sut = new Result<Customer>(string.Empty, HttpStatusCode.Forbidden);

            sut.IsSuccess.Should().BeFalse();
        }
    }
}