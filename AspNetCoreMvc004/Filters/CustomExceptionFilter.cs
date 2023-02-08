using AspNetCoreMvc004.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace AspNetCoreMvc004.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var error = context.Exception.Message;

            context.Result = new RedirectToActionResult("error", "home", new ErrorViewModel() { Errors = new List<string> { "Unknown Error" } });
        }
    }
}
