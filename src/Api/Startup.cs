using FizzBuzz.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IFizzBuzzService fizzBuzzService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var query = context.Request.Query;

                bool hasFrom = false, hasTotal = false;
                int from = 1, total = 20;

                if (query != null)
                {
                    hasFrom = int.TryParse(query["from"], out from);
                    hasTotal = int.TryParse(query["total"], out total);
                }

                if (!hasFrom)
                {
                    from = 1;
                }
                if (!hasTotal)
                {
                    total = 20;
                }

                IEnumerable<string> result = fizzBuzzService.Play(from, total);
                await context.Response.WriteAsync(string.Join(", ", result));
            });
        }
    }
}
