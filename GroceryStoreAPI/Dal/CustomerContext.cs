﻿using GroceryStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Dal
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }
        
        public DbSet<Customer> Customers { get; set; }
        
    }
}