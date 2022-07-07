using System;
using System.Net;
using System.Threading.Tasks;

using CoffeeMachine.Application.Exceptions.CustomExceptions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace CoffeeMachine.Web
{
    /// <summary>
    /// custom middleware for exceptions that occur during application execution
    /// </summary>
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// call next middleware or handle exception
        /// </summary>
        /// <param name="context">request of client</param>
        /// <param name="next">ref of next middleware</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotEnoughMoneyException notMoneyEx)
            {
                var logMessage = "money of client less than price of coffee";
                await CreateResponseAsync(notMoneyEx, _env, context, logMessage, (int)HttpStatusCode.BadRequest);
            }
            catch (NullCashboxException nullCashboxEx)
            {
                var logMessage = "In cashbox of coffee machine not enough money!";
                await CreateResponseAsync(nullCashboxEx, _env, context, logMessage, (int)HttpStatusCode.InternalServerError);
            }
            catch (NullReferenceException nullEx)
            {
                var logMessage = "Nof found object";
                await CreateResponseAsync(nullEx, _env, context, logMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var logMessage = "Unknown error";
                await CreateResponseAsync(ex, _env, context, logMessage, (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// prepares and sends a response to the client when an exception occurs
        /// </summary>
        /// <param name="ex">exception</param>
        /// <param name="env">environment variables</param>
        /// <param name="context">request from client</param>
        /// <param name="logMessage">message that write in console</param>
        /// <param name="statusCode">status code of response</param>
        /// <returns></returns>
        private async Task CreateResponseAsync(Exception ex, IWebHostEnvironment env, HttpContext context,
            string logMessage, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            Log.Error(logMessage);
            Log.Error(ex.ToString());

            if (env.IsDevelopment())
                await context.Response.WriteAsJsonAsync($"{ex}");
            else
                await context.Response.WriteAsJsonAsync(logMessage);
        }
    }
}