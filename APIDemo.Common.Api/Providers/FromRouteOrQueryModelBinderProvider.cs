namespace APIDemo.Common.Api.Providers
{
    using APIDemo.Common.Api.ModelBinders;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using System;

    public class FromRouteOrQueryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.IsComplexType)
            {
                return null;
            }

            return new BinderTypeModelBinder(typeof(FromRouteOrQueryModelBinder));
        }
    }

}
