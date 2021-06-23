using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;
using Serilog;

namespace GroceryStoreAPI.Commands
{
    public class NewCustomerCommand
    {
        private readonly CustomerContext _context;
        private readonly IValidator<NewCustomerRequest> _validator;

        public NewCustomerCommand(IValidator<NewCustomerRequest> validator, CustomerContext context)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<Customer>> Execute(NewCustomerRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return new Result<Customer>(result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty,
                    HttpStatusCode.BadRequest);
            }

            try
            {
                var customer = new Customer
                {
                    name = request.Name
                };

                var persisted = _context.Customers.Add(customer);

                await _context.SaveChangesAsync();

                return new Result<Customer>(persisted.Entity);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Failed to create new customer.");
                return new Result<Customer>(ErrorMessages.ExecutionFailed, HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}