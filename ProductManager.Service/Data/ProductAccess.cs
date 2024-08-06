using System.Reflection;
using System.Text.Json;
using ProductManager.Exceptions;

namespace ProductManager.Data
{
    public class ProductAccess : IProductAccess
    {
        // Load initial data from .json file
        private static List<Models.Product>? _products = new List<Models.Product>();

        public ProductAccess() { 
            // If no data is loaded, then initialize from the .json file to emulate a database load.
            if (_products.Count == 0)
            {
                string jsonFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Products.json");
                try
                {
                    using (StreamReader sr = new StreamReader(jsonFile))
                    {
                        string json = sr.ReadToEnd();
                        Console.WriteLine("json: " + json);
                        _products = JsonSerializer.Deserialize<List<Models.Product>>(json);
                        Console.WriteLine("Loaded Products.json file successfully from location[" + jsonFile + ']');
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to read Products.json! location[" + jsonFile + ']');
                    Console.WriteLine(e.Message);
                }
            }
        }

        // Get list of Products
        public List<Models.Product> List()
        {
            return _products;
        }

        // Get Product by Id
        public Models.Product GetById(int? id)
        {
            if (id != null)
            {
                return _products.Find(p => p.ID == id.Value);
            }
            return null;
        }

        // Create a new Product
        public void Create(Models.Product product) {
            if (product == null) throw new ProductDataException(@"[invalid product]");
            if (_products.Exists(p => p.Name == product.Name)) throw new ProductDataException($"[product with name[{product.Name}] already exists]");
            try
            {
                _products.Add(product);
            }
            catch (Exception e) {
                throw new ProductDataException($"[unable to add product with id[{product.ID}]]", e);
            }
        }

        // Remove existing product with matching id
        public int Remove(int? id)
        {
            if (id == null) throw new ProductDataException(@"[invalid product]");
            try
            {
                return _products.RemoveAll(p => p.ID == id.Value);
            }
            catch (Exception e)
            {
                throw new ProductDataException($"[error while removing product with id[{id}]]", e);
            }
        }

        // Update an existing product
        public int Update(int? id, Models.Product product)
        {
            if (id.HasValue)
            {
                var p = _products.Find(x => x.ID == id.Value);
                if (p == null)
                {
                    throw new ProductDataException(@"[product not found]");
                }
                else {
                    p.Name = product.Name;
                    p.Price = product.Price;
                    p.Quantity = product.Quantity;
                }
                return 1;
            }
            else
            {
                throw new ProductDataException(@"[product not found]");
            }
        }

    }
}
