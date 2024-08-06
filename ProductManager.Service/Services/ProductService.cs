using ProductManager.Data;
using ProductManager.Models;
using ProductManager.Exceptions;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private IProductAccess productAccess;

        public ProductService(ILogger<ProductService> logger, IProductAccess productAccess)
        {
            _logger = logger;
            this.productAccess = productAccess;
        }

        public void Create(Product product)
        {
            _logger.LogDebug($"[Create product[{product}]]");
            validateProduct(product);
            productAccess.Create(product);
            _logger.LogDebug($"[Create response]");
        }

        public Product GetById(int? id)
        {
            _logger.LogDebug($"[GetById id[{id}]]");
            var product = productAccess.GetById(id);
            _logger.LogDebug($"[GetById response[{product}]]");
            return product;
        }

        public List<Product> List()
        {
            _logger.LogDebug($"[List]");
            var products = productAccess.List();
            _logger.LogDebug($"{products.Count} products");
            return products;
        }

        public int Remove(int? id)
        {
            _logger.LogDebug($"[Remove id[{id}]]");
            var response = productAccess.Remove(id);
            _logger.LogDebug($"[Remove response[{response}]]");
            return response;
        }

        public int Update(int? id, Product product)
        {
            int response = 0;
            _logger.LogDebug($"[Update id[{id}], product[{product}]]");
            validateProduct(product);
            response = productAccess.Update(id, product);
            _logger.LogDebug($"[Update response[{response}]");
            return response;
        }

        public void validateProduct(Product product)
        {
            _logger.LogDebug($"[isValid check for product[{product}]]");
            if (product == null) 
                throw new ProductServiceException($"product invalid");
            if (product.Price != null && product.Price < 0) 
                throw new ProductServiceException($"product price must be a positive value");
            if (product.Quantity != null && product.Quantity < 0)
                throw new ProductServiceException($"product quantity must be a non-negative value");
        }
    }
}
