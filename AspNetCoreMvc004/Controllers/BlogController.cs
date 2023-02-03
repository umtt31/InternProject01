using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc004.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Article(string name, int id)
        {
            // var routers = Request.RouteValues["article"];   


            return View();
        }
    }
}
