using System;
using System.Collections.Generic;
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
        private const string TypeKey = "type";
        private const string AuthRootKey = "x-amazon-apigateway-auth";

        private AuthType _authType;

        public AuthType AuthType
        {
            get => _authType;
            set { _authType = value; OnPropertyChanged(); }
        }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var result = new Dictionary<string, IOpenApiAny>();

            if (HasPropertyChanged(nameof(AuthType)))
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
