using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;

// ReSharper disable CommentTypo

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayKeySourceOptions : AbstractOptions
    {
        private const string ApiKeySourceKey = "x-amazon-apigateway-api-key-source";

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key. This API-level property is a String type.
        /// Specify the source of the API key for requests.Valid values are:
        /// HEADER for receiving the API key from the X-API-Key header of a request.
        /// AUTHORIZER for receiving the API key from the UsageIdentifierKey from a Lambda authorizer(formerly known as a custom authorizer).
        /// </summary>
        public ApiKeySource? ApiKeySource { get; set; }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var result = new Dictionary<string, IOpenApiAny>();

            if (ApiKeySource.HasValue)
            {
                result[ApiKeySourceKey] = new OpenApiString(Enum.GetName(typeof(ApiKeySource), ApiKeySource.Value)?.ToUpper());
            }

            return result;
        }
    }
}