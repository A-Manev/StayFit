using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StayFit.Web.Areas.Identity.IdentityHostingStartup))]

namespace StayFit.Web.Areas.Identity
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
