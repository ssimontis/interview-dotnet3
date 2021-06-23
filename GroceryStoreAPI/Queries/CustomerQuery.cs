using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Validators;

namespace GroceryStoreAPI.Queries
{
    public class CustomerQuery
    {
        private readonly IValidator<CustomerQueryRequest> _validator;
        private readonly CustomerContext _context;

        public CustomerQuery(IValidator<CustomerQueryRequest> validator, CustomerContext context)
        {
            _validator = validator;
            _context = context;
        }
        
        /// <summary>
        /// Fetches an individual customer, if they exist.
        /// </summary>
        /// <returns>
        /// Returns the proper HTTP status code and an error message for invalid requests or the customer.
        /// </returns>
        public async Task<Result<Customer>> Execute(CustomerQueryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new Result<Customer>(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty, 
                    HttpStatusCode.BadRequest);
            }

            try
            {
                var customer = await _context.Customers.FindAsync(request.Id);

                return customer == null
                    ? new Result<Customer>(ErrorMessages.CustomerNotFound, HttpStatusCode.NotFound)
                    : new Result<Customer>(customer);
            }
            catch (Exception e)
            {
                return new Result<Customer>(ErrorMessages.ExecutionFailed, HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}