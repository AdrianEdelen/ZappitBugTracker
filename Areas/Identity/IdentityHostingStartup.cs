using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ZappitBugTracker.Areas.Identity.IdentityHostingStartup))]
namespace ZappitBugTracker.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}