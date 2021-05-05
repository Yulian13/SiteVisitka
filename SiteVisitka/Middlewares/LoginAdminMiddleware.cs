using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SiteVisitka.Middlewares
{
    public class LoginAdminMiddleware
    {
        private const string _inputStatus = "input";
        private const string _inputOK = "OK";

        private readonly RequestDelegate _next;

        public LoginAdminMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            var y = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            PathString url = context.Request.Path;
            if (url.Value.Equals("/Admin"))
            {
                context.Response.Redirect("/Admin/Index");
            }
            else if (url.StartsWithSegments("/Admin") && !url.Value.Contains("/Index")
                    && !(context.Session.GetString(_inputStatus)?.Equals(_inputOK) ?? false))
            {
                var t = url.Value.Split('/');
                string attrebut = (t.Length > 2) ? $"?action={t?[2]}" : "";
                string newUrl = $"/Admin/Index" + attrebut;
                context.Response.Redirect(newUrl);
            }
            else
             await _next.Invoke(context);
        }
    }
}
