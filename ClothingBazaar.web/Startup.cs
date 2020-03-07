using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClothingBazaar.web.Startup))]
namespace ClothingBazaar.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
