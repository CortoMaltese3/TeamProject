using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamProject.Startup))]
namespace TeamProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
