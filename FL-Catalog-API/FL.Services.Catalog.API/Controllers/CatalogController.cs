using FL.Common.Models;
using FL.Services.Catalog.Managers;
using Microsoft.AspNetCore.Mvc;

namespace FL.Services.Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : Controller
    {
        private readonly ICatalogManager _catalogManager;

        private ILogger<CatalogController> _logger;

        public CatalogController(
            ICatalogManager catalogManager,

            ILogger<CatalogController> logger)
        {
            _catalogManager = catalogManager;

            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> result = await _catalogManager.GetAllProductsAsync();

            return result;
        }

        [HttpPost]
        [Route("check")]
        public async Task CheckProductAvailability(Product product)
        {
            await _catalogManager.CheckProductAvailabilityAsync(product);
        }
    }
}
