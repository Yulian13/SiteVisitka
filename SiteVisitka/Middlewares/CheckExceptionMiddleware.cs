using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SiteVisitka.Serviсes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Middlewares
{
    public class CheckExceptionMiddleware
    {
        private readonly MyFileLogger _logger;
        private readonly RequestDelegate _next;

        public CheckExceptionMiddleware(RequestDelegate next, MyFileLogger logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if(exceptionHandlerPathFeature != null)
            {
                _logger.LogException(exceptionHandlerPathFeature.Error);
            }

            await _next.Invoke(context);
        }
    }
}
