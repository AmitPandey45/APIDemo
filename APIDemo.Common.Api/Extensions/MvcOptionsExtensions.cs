using APIDemo.Common.Api.Providers;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Common.Api.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static void AddRouteOrQueryProviderOption(this MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new FromRouteOrQueryModelBinderProvider());
        }
    }
}
