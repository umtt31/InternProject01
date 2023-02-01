using AspNetCoreMvc004.Models;
using AspNetCoreMvc004.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc004.Views.Shared.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type = 0)
        {
            var viewModels = _context.Products.Select(x => new ProductListComponentViewModel() { Name = x.Name, Description = x.Description }).ToList();

            if (type == 0)
            {
                return View("Default", viewModels);
            }
            else
            {
                return View("Type2", viewModels);
            }
        }
            

    }
}
