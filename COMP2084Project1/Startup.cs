using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COMP2084Project1.Startup))]
namespace COMP2084Project1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
