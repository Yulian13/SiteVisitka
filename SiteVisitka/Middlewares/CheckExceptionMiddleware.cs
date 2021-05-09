using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Middlewares
{
    public class CheckExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public CheckExceptionMiddleware(RequestDelegate next, IOptions<ILogger> options)
        {
            _logger = options.Value;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if(exceptionHandlerPathFeature != null)
            {
                _logger.LogError(exceptionHandlerPathFeature.Error, "myMessagelog");
            }

            await _next.Invoke(context);
        }
    }
}
