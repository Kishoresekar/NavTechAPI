using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Filters;
using NLog.Web;
using Microsoft.AspNetCore;

namespace NavTechAPI.Filters
{
    public class ExceptionHandling : FilterAttribute,
        Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string[] args = null;
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();;
            if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
                
                filterContext.ExceptionHandled = true;
            }
            else
            {
                

            }
            logger.LogException(NLog.LogLevel.Error, filterContext.Exception.Message,filterContext.Exception);

            
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Program>()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .UseNLog();
    }
}

