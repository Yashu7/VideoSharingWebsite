using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoSharingSystem.Startup))]
namespace VideoSharingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
