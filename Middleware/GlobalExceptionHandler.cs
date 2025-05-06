using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Week3Day3.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception e)
            {   //navigate to /Home/IntentionalError to see the message displayed on the screen and intentionally throw the exception

                Log.Error(e, "Unhandled exception occurred."); //logging error using Serilog
                context.Response.StatusCode = StatusCodes.Status500InternalServerError; //noticed that without manually setting the status, upon inspecting it showed the status as "200 OK" even though an exception is there.
                await context.Response.WriteAsync("Something went wrong! Please try again later.");
            }
        }
    }
}
