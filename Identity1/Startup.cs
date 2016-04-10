using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identity1.Startup))]
namespace Identity1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
