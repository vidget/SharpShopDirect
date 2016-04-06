using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SharpShopDirect.Startup))]
namespace SharpShopDirect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
