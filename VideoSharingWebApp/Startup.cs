using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoSharingWebApp.Startup))]
namespace VideoSharingWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
