using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProductManager.Data;
using ProductManager.Exceptions;
using ProductManager.Services;
using System.IO.IsolatedStorage;

namespace ProductManager.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;
        private readonly Mock<ILogger<ProductService>> _mockLogger_ProductService;
        private readonly Mock<ProductAccess> _productAccess_Mock;

        public ProductServiceTests()
        {
            _mockLogger_ProductService = new Mock<ILogger<ProductService>>();
            _productAccess_Mock = new Mock<ProductAccess>();
        }

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService(_mockLogger_ProductService.Object, _productAccess_Mock.Object);
        }

        [Test]
        public void IsList_Available()
        {
            var list = _productService.List();
            Assert.That(list.Count, Is.GreaterThan(0), "List size is not greater than zero.");
        }

        [Test]
        public void IsGetById_Found()
        {
            int id = 1000;
            var product = _productService.GetById(id);
            Assert.That(product.ID, Is.EqualTo(id));
        }

        [Test]
        public void IsCreate_Success()
        {
            var product = mockProduct_Good();
            _productService.Create(product);
        }

        [Test]
        public void IsCreate_Fail()
        {
            Assert.Throws<ProductServiceException>(delegate ()
            {
                var product = mockProduct_NegativePrice();
                _productService.Create(product);
            });
        }

        [Test]
        public void IsGetById_NotFound()
        {
            int id = 9999;
            var product = _productService.GetById(id);
            Assert.That(product, Is.Null);
        }

        [Test]
        public void IsUpdate_Success()
        {
            var product = _productService.GetById(1000);
            Assert.That(product, Is.Not.Null);

            product.Price = 1;
            _productService.Update(product.ID, product);
        }

        [Test]
        public void IsRemove_Success()
        {
            var product = _productService.GetById(1200);
            Assert.That(product, Is.Not.Null);

            _productService.Remove(product.ID);
        }

        [Test]
        public void IsValidateProduct_Success()
        {
            var product = mockProduct_Good();
            _productService.validateProduct(product);
        }

        [Test]
        public void IsValidateProduct_NegativePrice()
        {
            Assert.Throws<ProductServiceException>(delegate ()
            {
                var product = mockProduct_NegativePrice();
                _productService.validateProduct(product);
            });
        }

        [Test]
        public void IsValidateProduct_NegativeQuantity()
        {
            Assert.Throws<ProductServiceException>(delegate ()
            {
                var product = mockProduct_NegativeQuantity();
                _productService.validateProduct(product);
            });
        }



        private Models.Product mockProduct_Good()
        {
            var product = new Models.Product();
            product.ID = 1;
            product.Name = "test";
            product.Price = .50M;
            product.Quantity = 1;
            return product;
        }

        private Models.Product mockProduct_NegativePrice()
        {
            var product = new Models.Product();
            product.ID = 1;
            product.Name = "test";
            product.Price = -.50M;
            product.Quantity = 1;
            return product;
        }

        private Models.Product mockProduct_NegativeQuantity()
        {
            var product = new Models.Product();
            product.ID = 1;
            product.Name = "test";
            product.Price = .50M;
            product.Quantity = -1;
            return product;
        }
    }
}