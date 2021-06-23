using System.Collections.Generic;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Dto
{
    public class GetCustomersDto
    {
        public IList<Customer> Customers { get; set; }
    }
}