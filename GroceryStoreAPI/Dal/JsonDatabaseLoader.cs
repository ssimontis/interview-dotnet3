using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Dal
{
    /// <summary>
    /// Performs custom initialization logic to load data into the in-memory data store upon application
    /// initialization. Since EF Core's seed feature is designed for use with migrations, the existing
    /// functionality is inappropriate for a simple in-memory model.
    ///
    /// This will cause concurrency issues if multiple instantiations of the application are run.
    /// </summary>
    public class JsonDatabaseLoader : IDatabaseLoader
    {
        private readonly IServiceScopeFactory _scopedServiceFactory;

        public JsonDatabaseLoader(IServiceScopeFactory scopedServiceFactory)
        {
            _scopedServiceFactory = scopedServiceFactory;
        }

        public void LoadData()
        {
            using var scope = _scopedServiceFactory.CreateScope();

            using var context = scope.ServiceProvider.GetService<CustomerContext>();

            if (context.Customers.Any())
            {
                return;
            }

            var customers = ReadCustomers(scope);
            
            context.Customers.AddRange(customers.customers);
            context.SaveChanges();
        }

        private JsonCustomer ReadCustomers(IServiceScope scope)
        {
            // var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            // var dataPath = config.GetValue<DataStore>(DataStore.ConfigSection);
            // var dataFile = dataPath.JsonFile;

            // I/O exceptions are not caught because failing to load initial data should be considered a fatal
            // program error for this application.
            var dataContents = File.ReadAllText("database.json");

            return JsonSerializer.Deserialize<JsonCustomer>(dataContents);
        }
    }
}