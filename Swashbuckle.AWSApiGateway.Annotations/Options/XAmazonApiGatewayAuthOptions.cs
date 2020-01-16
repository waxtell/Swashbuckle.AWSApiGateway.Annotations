using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AWSApiGateway.Annotations.Enums;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public class XAmazonApiGatewayAuthOptions : AbstractExtensionOptions
    {
        private const string TypeKey = "type";
        private const string AuthRootKey = "x-amazon-apigateway-auth";

        public AuthType AuthType { get; set; }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            var children = new OpenApiObject
            {
                [TypeKey] = new OpenApiString(Enum.GetName(typeof(AuthType), AuthType)),
            };

            return new Dictionary<string, IOpenApiExtension>()
            {
                { AuthRootKey, children }
            };
        }
    }
}
