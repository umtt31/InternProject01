using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspNetCoreMvc004.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Before Action Executed");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("After Action Executed");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("Before Action Finished");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("After Action Finished");
        }
    }
}
