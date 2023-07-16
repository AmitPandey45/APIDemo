using APIDemo.Common.Api.Providers;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Common.Api.Extensions
{
    public static class CommonServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpContextAccessorService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddCommonAPIServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
