using System.Collections.Generic;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Dal
{
    public class JsonCustomer
    {
        public IList<Customer> customers { get; set; }
    }
}