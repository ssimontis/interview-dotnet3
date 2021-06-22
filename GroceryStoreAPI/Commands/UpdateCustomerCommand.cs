﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Commands
{
    public class UpdateCustomerCommand
    {
        private readonly IValidator<UpdateCustomerRequest> _validator;
        private readonly CustomerContext _context;

        public UpdateCustomerCommand(IValidator<UpdateCustomerRequest> validator, CustomerContext context)
        {
            _validator = validator;
            _context = context;
        }

        public async Task<Result<Customer>> Execute(UpdateCustomerRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return new Result<Customer>(result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty,
                    HttpStatusCode.BadRequest);
            }

            try
            {
                var customer = await _context.Customers.FindAsync(request.Id);

                if (customer == null)
                {
                    return new Result<Customer>(ErrorMessages.CustomerNotFound, HttpStatusCode.NotFound);
                }

                customer.Name = request.Name;

                await _context.SaveChangesAsync();

                return new Result<Customer>(customer);
            }
            catch(Exception e)
            {
                return new Result<Customer>(ErrorMessages.ExecutionFailed, HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}