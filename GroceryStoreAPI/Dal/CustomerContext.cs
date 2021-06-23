using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Dal
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        // Constructor for allowing fakes when testing
        public CustomerContext()
        {
            
        }
        
        // Virtual is required for fakes to function correctly
        public virtual DbSet<Customer> Customers { get; set; }

        // This was moved to its own method to allow use of fakes in unit tests
        public virtual async Task<IList<Customer>> AllCustomers() => await Customers.ToListAsync();

    }
}