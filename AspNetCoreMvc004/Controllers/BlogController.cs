using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc004.Controllers
{
    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {
        [HttpGet("{name}/{id}")]
        public IActionResult Article(string name, int id)
        {
            // var routers = Request.RouteValues["article"];   


            return View();
        }
    }
}
