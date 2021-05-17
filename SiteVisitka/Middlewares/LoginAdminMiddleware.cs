using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SiteVisitka.Serviсes;
using System.Threading.Tasks;

namespace SiteVisitka.Middlewares
{
    public class LoginAdminMiddleware
    {
        private readonly string _controller;
        private readonly string _UrlInputPass;

        private readonly RequestDelegate _next;
        private readonly ManagerLoginAdmin _managerLoginAdmin;

        public LoginAdminMiddleware(RequestDelegate next, IConfiguration configuration, ManagerLoginAdmin managerLoginAdmin)
        {
            _controller = configuration["AdminUrls:controller"];
            _UrlInputPass = configuration["AdminUrls:inputPass"];
            _next = next;
            _managerLoginAdmin = managerLoginAdmin;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            PathString url = context.Request.Path;
            if (url.Value.Equals(_controller))
            {
                context.Response.Redirect(_UrlInputPass);
            }
            else if (url.StartsWithSegments(_controller) && !url.Value.Contains(_UrlInputPass)
                    && !_managerLoginAdmin.IsStatusOK(context))
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
