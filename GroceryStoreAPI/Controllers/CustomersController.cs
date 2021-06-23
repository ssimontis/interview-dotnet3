using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Commands;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly NewCustomerCommand _newCustomerCommand;
        private readonly UpdateCustomerCommand _updateCustomerCommand;
        private readonly CustomerQuery _customerQuery;
        private readonly CustomersQuery _customersQuery;

        public CustomersController(NewCustomerCommand newCustomerCommand,
            UpdateCustomerCommand updateCustomerCommand,
            CustomerQuery customerQuery,
            CustomersQuery customersQuery)
        {
            _newCustomerCommand = newCustomerCommand;
            _updateCustomerCommand = updateCustomerCommand;
            _customerQuery = customerQuery;
            _customersQuery = customersQuery;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var getCustomerRequest = new CustomerQueryRequest
            {
                Id = id
            };

            var result = await _customerQuery.Execute(getCustomerRequest);

            return result.IsSuccess 
                ? result.Data 
                : StatusCode((int) result.StatusCode);
        }

        [HttpGet]
        public async Task<ActionResult<GetCustomersDto>> GetCustomers()
        {
            var result = await _customersQuery.Execute();

            return result.IsSuccess 
                ? new GetCustomersDto { Customers = result.Data} 
                : StatusCode((int) result.StatusCode);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(long id, UpdateCustomerDto dto)
        {
            var request = new UpdateCustomerRequest
            {
                Id = id,
                Name = dto?.Message
            };

            var result = await _updateCustomerCommand.Execute(request);

            return result.IsSuccess 
                ? result.Data 
                : StatusCode((int) result.StatusCode);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(NewCustomerRequest request)
        {
            var result = await _newCustomerCommand.Execute(request);

            return result.IsSuccess
                ? result.Data
                : StatusCode((int) result.StatusCode);
        }
    }
}