using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayRequestValidators : AbstractOptions
    {
        private const string RequestValidatorsRootKey = "x-amazon-apigateway-request-validators";

        public IEnumerable<RequestValidator> RequestValidators { get; set; }
        
        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var children = new  OpenApiObject();
            var result = new Dictionary<string, IOpenApiAny>();

            if (RequestValidators != null && RequestValidators.Any())
            {
                foreach (var validator in RequestValidators)
                {
                    children.Add(validator.ToDictionaryItem());
                }

                result[RequestValidatorsRootKey] = children;
            }

            return result;
        }
    }
}