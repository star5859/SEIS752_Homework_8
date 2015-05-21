using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SEIS752_MVC_WebApp_stark.Startup))]
namespace SEIS752_MVC_WebApp_stark
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
