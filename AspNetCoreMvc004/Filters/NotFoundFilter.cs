using AspNetCoreMvc004.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreMvc004.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public NotFoundFilter(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First();

            var id = (int)idValue;

            var hasProduct = _context.Products.Any(x => x.Id == id);

            if (hasProduct == false)
            {
                context.Result = new RedirectToActionResult("error", "home", new ErrorViewModel() { Errors = new List<string> { $"We can not found any object data with given id({id}) on database" } });
            }
        }
    }
}
