using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AWSApiGateway.Annotations;
using Swashbuckle.AWSApiGateway.Annotations.Enums;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;

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
                            .WithKeySource(ApiKeySource.Header)
                            .WithBinaryMediaTypes
                            (
                                bmtOptions => bmtOptions.BinaryMediaTypes = new[] {MediaTypeNames.Image.Jpeg}    
                            )
                            .WithRequestValidators
                            (
                                rv => rv.RequestValidators = new []
                                {
                                    new RequestValidator("basic")
                                    {
                                        ValidateRequestParameters = true,
                                        ValidateRequestBody = true
                                    },
                                    new RequestValidator("params-only")
                                    {
                                        ValidateRequestBody = false,
                                        ValidateRequestParameters = true
                                    }
                                }
                            )
                            .WithRequestValidator(rvo => rvo.RequestValidator = "basic");
                    }
                );

                c.AddXAmazonApiGatewayOperationAnnotations
                (
                    op =>
                    {
                        op
                            .WithIntegration
                            (
                                intOpt =>
                                {
                                    intOpt.Type = IntegrationType.http_proxy;
                                    intOpt.BaseUri = "https://your.domain.com";
                                }
                            )
                            .WithAuth
                            (
                                authOpt => authOpt.AuthType = AuthType.NONE
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

            app
                .UseSwagger
                (
                    c =>
                    {
                        c.PreSerializeFilters.Add((swagger, httpReq) =>
                        {
                            swagger.Servers = new List<OpenApiServer>
                            {
                                new OpenApiServer
                                {
                                    Url = "https://www.yourcustomdomain.com{basePath}"
                                }
                                .WithVariable("basePath", new OpenApiServerVariable { Default = "/sample"})
                                .AsRegionalEndpoint()
                                //.AsEdgeEndpoint("yourcustomdomain.com")
                                //.AsPrivateEndpoint("vpcid1", "vpcid2", "vpcid3")
                                //.AsRegionalEndpoint("yourcustomdomain.com")
                            };
                        });
                    }
                );

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(x =>
            {
                x.DocumentTitle = "SampleApp9000";
                x.SwaggerEndpoint("/swagger/sampleapp/swagger.json", "Sample App 9000");
                x.RoutePrefix = string.Empty;
            });

            app
                .UseEndpoints
                (
                    endpoints => { endpoints.MapControllers(); }
                );
        }
    }
}
