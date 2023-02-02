using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc004.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
