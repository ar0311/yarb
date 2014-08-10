using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Yarb.Web.Startup))]
namespace Yarb.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
