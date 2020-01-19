using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AWSApiGateway.Annotations;

namespace SampleApp9000
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    "sampleapp",
                    new OpenApiInfo
                    {
                        Title = "Sample App 9000",
                        Version = "1.0.0",
                        Description = "Provides a simple example of the tool"
                    }
                );

                c.AddXAmazonApiGatewayAnnotations
                (
                    options =>
                    {
                        options
                            .WithKeySource
                            (
                                kso =>
                                {
                                    kso.ApiKeySource = ApiKeySource.Header;
                                }
                            );
                    }
                );

                c.AddXAmazonApiGatewayOperationAnnotations
                (
                    op =>
                    {
                        op.WithIntegration
                        (
                        intOpt =>
                            {
                                intOpt.Type = IntegrationType.http_proxy;
                                intOpt.BaseUri = "https://your.domain.com";
                            }
                        );
                    }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger(c =>
            {
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "SampleApp9000";
                c.SwaggerEndpoint("/swagger/sampleapp/swagger.json", "Sample App 9000");
                c.RoutePrefix = string.Empty;
            });

            app
                .UseEndpoints
                (
                    endpoints =>
                    {
                        endpoints.MapControllers();
                    }
                );
        }
    }
}
