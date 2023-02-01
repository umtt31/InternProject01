using System.Reflection.Metadata.Ecma335;

namespace AspNetCoreMvc004.PartialViews
{
    public class ProductListPartialViewModel
    {
        public List<ProductPartialViewModel> Products { get; set; }

    }

    public class ProductPartialViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
