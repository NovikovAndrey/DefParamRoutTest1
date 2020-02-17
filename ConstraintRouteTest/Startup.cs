using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConstraintRouteTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var routeHandler = new RouteHandler(Handler);
            var routeBuilder = new RouteBuilder(app, routeHandler);

            routeBuilder.MapRoute("default", "{controller}/{action}/{id?}", null, new { action = "Index" });
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context)=>
            {
                await context.Response.WriteAsync("Default");
            });
        }

        private async Task Handler(HttpContext context)
        {
            await context.Response.WriteAsync("Constraints Route");
        }
    }
}
