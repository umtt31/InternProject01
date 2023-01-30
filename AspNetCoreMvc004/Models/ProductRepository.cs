namespace AspNetCoreMvc004.Models
{
    public class ProductRepository
    {

        private static List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name = "Kalem 1", Price = 100, Stock = 200},
            new Product { Id = 2, Name = "Kalem 2", Price = 200, Stock = 300},
            new Product { Id = 3, Name = "Kalem 3", Price = 300, Stock = 400},
        };

        public List<Product> GetAll() => _products;

        public void Add(Product newProduct) => _products.Add(newProduct);

        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == id);

            if (hasProduct == null)
            {
                throw new Exception($"With {id} id there is no product...");
            }

            _products.Remove(hasProduct);
        }

        public void Update(Product updateProduct)
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == updateProduct.Id);

            if (hasProduct == null)
            {
                throw new Exception($"With {updateProduct.Id} id there is no product...");
            }

            hasProduct.Id = updateProduct.Id;
            hasProduct.Name = updateProduct.Name;
            hasProduct.Price = updateProduct.Price;
            hasProduct.Stock = updateProduct.Stock; 

            var index = _products.FindIndex(x => x.Id == updateProduct.Id);

            _products[index] = hasProduct;
        }
    }
}
