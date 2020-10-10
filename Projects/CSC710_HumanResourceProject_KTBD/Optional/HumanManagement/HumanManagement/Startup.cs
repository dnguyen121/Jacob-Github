using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HumanManagement.Startup))]
namespace HumanManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
