using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreMvc004.Filters
{
    public class CacheResourseFilter : Attribute, IResourceFilter
    {
        private static IActionResult _cache;

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _cache = context.Result;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.Result = _cache;
        }
    }
}
