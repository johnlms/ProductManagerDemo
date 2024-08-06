namespace ProductManager.Data
{
    public interface IProductAccess
    {
        // Get list of Products
        public List<Models.Product> List();

        // Get Product by Id
        public Models.Product GetById(int? id);

        // Create a new Product
        public void Create(Models.Product product);

        // Remove existing product with matching id
        public int Remove(int? id);

        // Update an existing product
        public int Update(int? id, Models.Product product);
    }
}
