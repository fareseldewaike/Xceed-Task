using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL;

namespace ProductCatalog.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ProductCatalogDbContext>(options =>
                      options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(
                //options =>
                //{
                //    options.User.AllowedUserNameCharacters =
                //         "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
                //}
                )
                .AddEntityFrameworkStores<ProductCatalogDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
