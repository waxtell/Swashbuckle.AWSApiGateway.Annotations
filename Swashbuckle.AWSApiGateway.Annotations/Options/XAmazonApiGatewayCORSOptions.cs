using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayCORSOptions : AbstractExtensionOptions
    {
        private const string CORSRootKey = "x-amazon-apigateway-cors";
        private const string AllowOriginsKey = "allowOrigins";
        private const string AllowCredentialsKey = "allowCredentials";
        private const string ExposeHeadersKey = "exposeHeaders";
        private const string MaxAgeKey = "maxAge";
        private const string AllowMethodsKey = "allowMethods";
        private const string AllowHeadersKey = "allowHeaders";

        /// <summary>
        /// Specifies the allowed origins.
        /// </summary>
        public IEnumerable<string> AllowOrigins { get; set; }
        /// <summary>
        /// Specifies the headers that are exposed.
        /// </summary>
        public IEnumerable<string> ExposeHeaders { get; set; }
        /// <summary>
        /// Specifies the number of seconds that the browser should cache preflight request results.
        /// </summary>
        public int? MaxAge { get; set; }
        /// <summary>
        /// Specifies whether credentials are included in the CORS request.
        /// </summary>
        public bool? AllowCredentials { get; set; }
        /// <summary>
        /// Specifies the allowed HTTP methods.
        /// </summary>
        public IEnumerable<string> AllowMethods { get; set; }

        /// <summary>
        /// Specifies the allowed headers.
        /// </summary>
        public IEnumerable<string> AllowHeaders { get; set; }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            var children = new OpenApiObject();

            if (AllowOrigins != null && AllowOrigins.Any())
            {
                var allowOrigins = new OpenApiArray();
                allowOrigins.AddRange(AllowOrigins.Select(x => new OpenApiString(x)));

                children[AllowOriginsKey] = allowOrigins;
            }

            if (AllowCredentials.HasValue)
            {
                children[AllowCredentialsKey] = new OpenApiBoolean(AllowCredentials.Value);
            }

            if (ExposeHeaders != null && ExposeHeaders.Any())
            {
                var exposeHeaders = new OpenApiArray();
                exposeHeaders.AddRange(ExposeHeaders.Select(x => new OpenApiString(x)));

                children[ExposeHeadersKey] = exposeHeaders;
            }

            if (MaxAge.HasValue)
            {
                children[MaxAgeKey] = new OpenApiInteger(MaxAge.Value);
            }

            if (AllowMethods != null && AllowMethods.Any())
            {
                var allowMethods = new OpenApiArray();
                allowMethods.AddRange(AllowMethods.Select(x => new OpenApiString(x)));

                children[AllowMethodsKey] = allowMethods;
            }

            if (AllowHeaders != null && AllowHeaders.Any())
            {
                var allowHeaders = new OpenApiArray();
                allowHeaders.AddRange(AllowHeaders.Select(x => new OpenApiString(x)));

                children[AllowHeadersKey] = allowHeaders;
            }

            return new Dictionary<string, IOpenApiExtension>()
            {
                { CORSRootKey, children }
            };
        }
    }
}