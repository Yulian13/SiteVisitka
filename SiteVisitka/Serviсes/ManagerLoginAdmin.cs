using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.Serviсes
{
    public class ManagerLoginAdmin
    {
        private const string _inputStatus = "input";
        private const string _inputOK = "OK";

        private readonly string _password;
        public ManagerLoginAdmin(IConfiguration configuration)
        {
            _password = configuration["Pass"];
        }

        public bool IsStatusOK(HttpContext context) => context.Session.GetString(_inputStatus)?.Equals(_inputOK) ?? false;

        public void SetStatus(string pass, HttpContext context) 
        {
            if (_password.Equals(pass))
                context.Session.SetString(_inputStatus, _inputOK);
        }

        public bool SetStatusAndCheckPassword(string pass, HttpContext context)
        {
            SetStatus(pass, context);
            return _password.Equals(pass);
        }
    }
}
