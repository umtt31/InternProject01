using System.Net;
using System.Runtime.CompilerServices;

namespace AspNetCoreMvc004.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private string _ipAddress = "::1";

        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var reqIpAddress = context.Connection.RemoteIpAddress;
            bool AnyWhiteIpAddress = IPAddress.Parse(_ipAddress).Equals(reqIpAddress);

            if (AnyWhiteIpAddress)
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Forbidden");
            }
        }
    }
}
