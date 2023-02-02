using AspNetCoreMvc004.Models;
using AspNetCoreMvc004.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc004.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {

        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public VisitorViewComponent(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string type)
        { 
            if (type == "table")
            {
                var visitors = _context.Visitors.ToList();
                var visitorsViewModel = _mapper.Map<List<VisitorViewModel>>(visitors);

                return View("Table", visitorsViewModel);
            } 
            else
            {
                return View("Default");
            }
            
        }
    }
}
