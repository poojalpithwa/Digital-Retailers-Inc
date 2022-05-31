using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ELaptop.Startup))]
namespace ELaptop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
