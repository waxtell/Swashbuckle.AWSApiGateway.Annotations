using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Swashbuckle.AWSApiGateway.Annotations.Enums;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public interface IXAmazonApiGatewayAuthOptions
    {
        AuthType AuthType { get; set; }
    }

    public class XAmazonApiGatewayAuthOptions : AbstractOptions, IXAmazonApiGatewayAuthOptions
    {
        private AuthType _authType;
        private const string TypeKey = "type";
        private const string AuthRootKey = "x-amazon-apigateway-auth";

        public AuthType AuthType
        {
            get => _authType;
            set { _authType = value; OnPropertyChanged(); }
        }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var result = new Dictionary<string, IOpenApiAny>();

            if (GetChangedProperties().Contains(nameof(AuthType)))
            {
                var children = new OpenApiObject
                {
                    [TypeKey] = new OpenApiString(Enum.GetName(typeof(AuthType), AuthType)),
                };

                result[AuthRootKey] = children;
            }

            return result;
        }
    }
}
