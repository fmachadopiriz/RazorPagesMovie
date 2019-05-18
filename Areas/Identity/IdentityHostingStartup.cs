using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Areas.Identity.Data;

[assembly: HostingStartup(typeof(RazorPagesMovie.Areas.Identity.IdentityHostingStartup))]
namespace RazorPagesMovie.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RazorPagesMovieIdentityDbContext>(options =>
                   options.UseSqlite(context.Configuration.GetConnectionString("MovieContext")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<RazorPagesMovieIdentityDbContext>();
            });
        }
    }
}