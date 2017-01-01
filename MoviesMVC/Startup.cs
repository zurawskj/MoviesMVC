using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviesMVC.Startup))]
namespace MoviesMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
