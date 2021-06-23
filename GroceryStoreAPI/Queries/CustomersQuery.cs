using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GroceryStoreAPI.Queries
{
    public class CustomersQuery
    {
        private readonly CustomerContext _context;

        public CustomersQuery(CustomerContext context)
        {
            _context = context;
        }
        
        public async Task<Result<IList<Customer>>> Execute()
        {
            try
            {
                return new Result<IList<Customer>>(await _context.AllCustomers());
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Failed to fetch customers.");
                return new Result<IList<Customer>>(ErrorMessages.ExecutionFailed, HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}