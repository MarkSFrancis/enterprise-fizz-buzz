using FizzBuzz.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FizzBuzz.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFizzBuzz();
            services.AddFizzBuzzLogging();
            services.AddTransient<IFizzBuzzQueryReader, FizzBuzzQueryReader>();
            services.AddTransient<IJsonSerializerService, JsonSerializerService>();
            services.AddTransient<IFizzBuzzRequestHandler, FizzBuzzRequestHandler>();
            services.AddTransient<IFizzBuzzResponseHandler, FizzBuzzResponseHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IFizzBuzzRequestHandler requestHandler,
                              IFizzBuzzResponseHandler responseHandler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var result = requestHandler.HandleRequest(context.Request);

                await responseHandler.WriteResponse(context.Response, result);
            });
        }
    }
}
