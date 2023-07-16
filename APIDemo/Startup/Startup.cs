using APIDemo.Common.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Startup
{
    public static class Startup
    {
        public static (WebApplicationBuilder builder, WebApplication app) StartApp(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            IServiceCollection services = ConfigureServices(builder, args);
            WebApplication app = ConfigureMiddleware(builder, args);

            return (builder, app);
        }

        public static IServiceCollection ConfigureServices(WebApplicationBuilder builder, string[] args)
        {
            IServiceCollection services = builder.Services;
            services.AddControllers();

            //services.AddControllers()
            //    .AddJsonOptions(options =>
            //    {
            //        options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            //    })
            //    .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressInferBindingSourcesForParameters = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services
                .AddHttpContextAccessorService();

            return services;
        }

        public static WebApplication ConfigureMiddleware(WebApplicationBuilder builder, string[] args)
        {
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //// app.UseRouting();
            ///
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            ////var options = new MessageHandlerMiddlewareOptions
            ////{
            ////    SystemAPILogInfo = SystemAPIConstant.LoggingMeetingSystemAPILogInfo
            ////};
            ////app.UseMiddleware<MessageHandlerMiddleware>(options);
            ////app.UseEndpoints(endpoints =>
            ////{
            ////    endpoints.MapControllers();
            ////});

            return app;
        }
    }
}
