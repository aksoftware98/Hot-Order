using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OneClickDelivery.Website.Startup))]
namespace OneClickDelivery.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
