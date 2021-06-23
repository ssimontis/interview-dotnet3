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

        /// <summary>
        /// Retrieves a single customer by their ID.
        /// </summary>
        /// <param name="id">The customer's ID (a 64-bit integer > 0)</param>
        /// <returns>The customer which the corresponding ID, or a 404 status.</returns>
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

        /// <summary>
        /// Returns a list of all customers in the database.
        /// </summary>
        /// <returns>A list of all customers (empty if none exist).</returns>
        [HttpGet]
        public async Task<ActionResult<GetCustomersDto>> GetCustomers()
        {
            var result = await _customersQuery.Execute();

            return result.IsSuccess 
                ? new GetCustomersDto { Customers = result.Data} 
                : StatusCode((int) result.StatusCode);
        }

        /// <summary>
        /// Updates an existing customer, as specified by their ID.
        /// </summary>
        /// <param name="id">The customer's ID (a 64-bit integer > 0)</param>
        /// <param name="dto">An object containing the name that must be updated.</param>
        /// <returns>The updated customer record.</returns>
        [HttpPut("{id:long}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(long id, UpdateCustomerDto dto)
        {
            var request = new UpdateCustomerRequest
            {
                Id = id,
                Name = dto?.Name
            };

            var result = await _updateCustomerCommand.Execute(request);

            return result.IsSuccess 
                ? result.Data 
                : StatusCode((int) result.StatusCode);
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="request">An object containing the name of the customer.</param>
        /// <returns>The customer object with an assigned ID.</returns>
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