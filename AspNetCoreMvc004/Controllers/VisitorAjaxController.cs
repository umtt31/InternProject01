using AspNetCoreMvc004.Models;
using AspNetCoreMvc004.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc004.Controllers
{
    public class VisitorAjaxController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public VisitorAjaxController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            var visitor = _mapper.Map<Visitor>(visitorViewModel);

            visitor.CreatedDate = DateTime.Now;
            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return Json(new { IsSuccess = "true" });
        }

        public IActionResult VisitorCommentList()
        {
            var visitors = _context.Visitors.ToList();
            var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);

            return Json(visitorViewModels); 
        }
    }
}
