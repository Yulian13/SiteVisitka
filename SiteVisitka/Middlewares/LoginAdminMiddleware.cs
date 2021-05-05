using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SiteVisitka.Middlewares
{
    public class LoginAdminMiddleware
    {
        private const string _inputStatus = "input";
        private const string _inputOK = "OK";

        private readonly string _controller;
        private readonly string _UrlInputPass;

        private readonly RequestDelegate _next;

        public LoginAdminMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _controller = configuration["AdminUrls:controller"];
            _UrlInputPass = configuration["AdminUrls:inputPass"];
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            PathString url = context.Request.Path;
            if (url.Value.Equals(_controller))
            {
                context.Response.Redirect(_UrlInputPass);
            }
            else if (url.StartsWithSegments(_controller) && !url.Value.Contains(_UrlInputPass)
                    && !(context.Session.GetString(_inputStatus)?.Equals(_inputOK) ?? false))
            {
                var t = url.Value.Split('/');
                string attrebut = (t.Length > 2) ? $"?action={t[2]}" : "";
                string newUrl = _UrlInputPass + attrebut;
                context.Response.Redirect(newUrl);
            }
            else
             await _next.Invoke(context);
        }
    }
}
