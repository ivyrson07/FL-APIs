using EasyNetQ;
using FL.Common.Models;
using FL.Services.Catalog.Models;

namespace FL.Services.Catalog.Managers
{
    public interface ICatalogManager
    {
        Task<List<Product>> GetAllProductsAsync();

        Task CheckProductAvailabilityAsync(Product product);
    }

    public class CatalogManager : ICatalogManager
    {
        private readonly IBus _bus;

        public CatalogManager(IBus bus)
        {
            _bus = bus;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            // call database here, beep, beep, beep, ...

            return await Task.FromResult(new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "FLPRODUCT001",
                    Name = "FL Product 001",
                    Price = (decimal)20.23
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "FLPRODUCT002",
                    Name = "FL Product 002",
                    Price = (decimal)5.34
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "FLPRODUCT003",
                    Name = "FL Product 003",
                    Price = (decimal)14.78
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "FLPRODUCT004",
                    Name = "FL Product 004",
                    Price = (decimal)35
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "FLPRODUCT005",
                    Name = "FL Product 005",
                    Price = (decimal)25
                }
            });
        }

        public async Task CheckProductAvailabilityAsync(Product product)
        {
            await _bus.Rpc.RespondAsync<Product, InventoryResponse>(request => Task.Factory.StartNew(() =>
            {
                // this part was supposed to be a call to another service to retrieve something before the client process the next step
                // let's just use a random number to give the client the remaining stock, 
                // and the client side would possibly call another service depending on the stock count, or just throw an error if the next step is not possible.
                int stock = new Random().Next(4);

                return new InventoryResponse
                {
                    Stock = stock
                };
            }));
        }
    }
}
