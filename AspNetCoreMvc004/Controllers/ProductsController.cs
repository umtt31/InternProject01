using AspNetCoreMvc004.Filters;
using AspNetCoreMvc004.Helpers;
using AspNetCoreMvc004.Models;
using AspNetCoreMvc004.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreMvc004.Controllers
{

    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private AppDbContext _context;

        private readonly ProductRepository _productRepository;

        private readonly IMapper _mapper;

        private readonly IFileProvider _fileProvider;

        public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _productRepository = new ProductRepository();
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;

            /* 
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product() { Name = "Kalem 1", Price = 100, Stock = 200, Color = "Red"});
                _context.Products.Add(new Product() { Name = "Kalem 2", Price = 200, Stock = 300 });
                _context.Products.Add(new Product() { Name = "Kalem 3", Price = 300, Stock = 400 });

                _context.SaveChanges();
            }
            */
        }

        public IActionResult Index(/* [FromServices]IHelper helper2, int id */)
        {
            List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id= x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                Description = x.Description,
                PublishDate = x.PublishDate,
                Color = x.Color,
                Expire = x.Expire,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath,
                IsPublish = x.IsPublish,
                
            }).ToList();

            return View(products);
        }

        [HttpGet("{page}/{pageSize}")]
        public IActionResult Pages(int page, int pageSize)
        {
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("{productid}", Name = "GetById")]
        public IActionResult GetById(int productid)
        {
            var product = _context.Products.Find(productid);

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("{id}", Name = "Remove")]
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id); // Primary Key'e gore arama yapiyor
            _context.Products.Remove(product);

            _context.SaveChanges();

            TempData["status"] = "Product deleted successfully...";

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>() { { "1 Month", 1 }, 
                                                             { "3 Month" , 3}, 
                                                             { "6 Month", 6 }, 
                                                             { "12 Month", 12 } };

            ViewBag.colorSelect = new SelectList(new List<ColorSelectList>
            {
                new() { Data = "Blue", Value = "Blue" },
                new() { Data = "Red", Value = "Red" },
                new() { Data = "Green", Value = "Green" },
            }, "Value", "Data");

            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

            return View();
        }


        [HttpPost] 
        public IActionResult Add(ProductViewModel newProduct)
        {
            /*
            var name = HttpContext.Request.Form["Name"].ToString();
            var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            var color = HttpContext.Request.Form["Color"].ToString();
            */

            // Product newProduct = new() { Name = Name, Price = Price, Stock = Stock, Color = Color};

            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(newProduct);

                    if (newProduct.Image != null && newProduct.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var RandomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, RandomImageName);


                        using var stream = new FileStream(path, FileMode.Create);
                        newProduct.Image.CopyTo(stream);

                        product.ImagePath = RandomImageName;
                    }

                    _context.Products.Add(product);
                    _context.SaveChanges();

                    TempData["status"] = "Product added successfully...";

                    return RedirectToAction("Index");
                } 
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "unknown error while saving");
                    result = View();
                }
            }
            else
            {
                result = View();
            }

            ViewBag.Expire = new Dictionary<string, int>() { { "1 Month", 1 },
                                                             { "3 Month" , 3},
                                                             { "6 Month", 6 },
                                                             { "12 Month", 12 } };

            ViewBag.colorSelect = new SelectList(new List<ColorSelectList> { new() { Data = "Blue", Value = "Blue" },
                                                                                 new() { Data = "Red", Value = "Red" },
                                                                                 new() { Data = "Green", Value = "Green" },
                                                                                }, "Value", "Data");
            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

            return result;

        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("{id}", Name = "Update")]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            // ViewBag.ExpireValue = product.Expire;

            ViewBag.Expire = new Dictionary<string, int>() { { "1 Month", 1 },
                                                             { "3 Month" , 3},
                                                             { "6 Month", 6 },
                                                             { "12 Month", 12 } };

            ViewBag.colorSelect = new SelectList(new List<ColorSelectList>
            {
                new() { Data = "Blue", Value = "Blue" },
                new() { Data = "Red", Value = "Red" },
                new() { Data = "Green", Value = "Green" },
            }, "Value", "Data", product.Color);

            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name", /* product.CaetegoryId !! bunu yamak icin prop ekliyoruz */product.Category.Id);

            return View(_mapper.Map<ProductUpdateViewModel>(product));
        }

        [HttpPost] 
        public IActionResult Update(ProductUpdateViewModel updatedProduct, int productId)
        {
            updatedProduct.Id = productId;

            if (!ModelState.IsValid)
            {
                var product = _context.Products.Find(productId);

                ViewBag.Expire = new Dictionary<string, int>() { { "1 Month", 1 },
                                                                 { "3 Month" , 3},
                                                                 { "6 Month", 6 },
                                                                 { "12 Month", 12 } };

                ViewBag.colorSelect = new SelectList(new List<ColorSelectList> {
                                                     new() { Data = "Blue", Value = "Blue" },
                                                     new() { Data = "Red", Value = "Red" },
                                                     new() { Data = "Green", Value = "Green" }, }, "Value", "Data", product.Color);
                return View();
            }

            if (updatedProduct.Image != null && updatedProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "images");

                var RandomImageName = Guid.NewGuid() + Path.GetExtension(updatedProduct.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, RandomImageName);


                using var stream = new FileStream(path, FileMode.Create);
                updatedProduct.Image.CopyTo(stream);

                updatedProduct.ImagePath = RandomImageName;
            }

            _context.Products.Update(_mapper.Map<Product>(updatedProduct));
            _context.SaveChanges();

            TempData["status"] = "Product updated successfully...";

            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")] public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());


            if (anyProduct)
            {
                return Json("There is a product with same name");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
