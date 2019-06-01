
using Microsoft.Owin;
using System.Web.Http;
using Owin;
using System.Globalization;
using System.Threading;

[assembly: OwinStartup(typeof(API.Servico.Startup))]
namespace API.Servico
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.Use(config);
        }
    }
}
