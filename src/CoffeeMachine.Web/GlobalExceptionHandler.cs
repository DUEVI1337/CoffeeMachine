using System;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace CoffeeMachine.Web
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    Log.Error(ex.ToString());
                    await context.Response.WriteAsJsonAsync(ex.ToString());
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var result = "Unknown error";
                    Log.Error(ex.Message);
                    await context.Response.WriteAsJsonAsync(result);
                }
            }
        }
    }
}