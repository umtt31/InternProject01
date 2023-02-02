using AspNetCoreMvc004.Models;
using AspNetCoreMvc004.PartialViews;
using AspNetCoreMvc004.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreMvc004.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;
        
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Select(x => new ProductPartialViewModel() {Id = x.Id, Name = x.Name, Price = x.Price, Stock = x.Stock,}).ToList();

            ViewBag.productsListPartialViewModel = new ProductListPartialViewModel() { Products = products };

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Visitor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorViewModel);
                visitor.CreatedDate = DateTime.Now;

                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                TempData["result"] = "Comment saved successfully";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
            catch (Exception)
            {
                TempData["result"] = "Error While saving comment";

                return RedirectToAction(nameof(HomeController.Visitor));
                throw;
            }
        }

        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            var delete = _context.Visitors.Find(id);
            _context.Visitors.Remove(delete);
            _context.SaveChanges();


            return RedirectToAction("Visitor");
        }
    }
}