namespace APIDemo.Common.Api.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Threading.Tasks;

    public class FromRouteOrQueryModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                var idParam = bindingContext.ActionContext.RouteData.Values[bindingContext.ModelName]?.ToString();
                if (string.IsNullOrEmpty(idParam))
                {
                    idParam = bindingContext.ActionContext.HttpContext.Request.Query[bindingContext.ModelName].ToString();
                }

                if (!string.IsNullOrEmpty(idParam))
                {
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                    bindingContext.Result = ModelBindingResult.Success(idParam);
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
    }
}
