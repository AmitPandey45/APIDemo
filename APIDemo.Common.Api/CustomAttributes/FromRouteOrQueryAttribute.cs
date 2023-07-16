namespace APIDemo.Common.Api.CustomAttributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FromRouteOrQueryAttribute : ModelBinderAttribute, IModelBinder
    {
        public FromRouteOrQueryAttribute()
            : base(typeof(FromRouteOrQueryAttribute))
        {
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                var paramValue = GetValueFromRouteData(bindingContext.ModelName, bindingContext);
                if (paramValue == null)
                {
                    paramValue = GetValueFromQueryString(bindingContext.ModelName, bindingContext);
                }

                if (paramValue != null)
                {
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                    bindingContext.Result = ModelBindingResult.Success(paramValue);
                }
                else
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }
            else
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                bindingContext.Result = ModelBindingResult.Success(valueProviderResult.FirstValue);
            }

            return Task.CompletedTask;
        }

        private static string GetValueFromRouteData(string paramName, ModelBindingContext bindingContext)
        {
            return bindingContext.ActionContext.RouteData.Values[paramName]?.ToString();
        }

        private static string GetValueFromQueryString(string paramName, ModelBindingContext bindingContext)
        {
            return bindingContext.ActionContext.HttpContext.Request.Query[paramName].ToString();
        }
    }
}
