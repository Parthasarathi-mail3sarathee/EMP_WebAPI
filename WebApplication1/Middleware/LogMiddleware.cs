using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Contracts;


namespace WebApplication_WebAPI.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogHeaders _logger;
        static string fileName;
      
        public LogMiddleware(RequestDelegate next, ILogHeaders logger)
        {
            fileName = @"F:\Partha\Code\Emp\EmployeeWebAPI\WebApplication1\logs\Middleware_logHeader_Each_Request_" + DateTime.Now.ToString("dd_MM_yy") + ".txt";
            _next = next;
            _logger = logger;
            _logger.setFileLog(fileName);
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.WriteLog("Log Started");
            HeaderLog(context);
            await _next.Invoke(context);
            _logger.WriteLogComplete();
        }

        private void HeaderLog(HttpContext context)
        {
            foreach (var key in context?.Request?.Headers?.Keys)
            {
                _logger.WriteLog(DateTime.Now.ToString("dd_MM_yy_hh_mm_ss") + " " + key + " : " + context?.Request?.Headers[key].ToString());
            }
        }
    }
}
