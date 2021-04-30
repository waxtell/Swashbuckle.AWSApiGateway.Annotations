using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    internal class XAmazonApiGatewayEndpointConfigurationOptions : AbstractOptions
    {
        private const string EndpointConfigurationRootKey = "x-amazon-apigateway-endpoint-configuration";
        private const string TypesKey = "types";
        private const string VpcEndpointIdsKey = "vpcEndpointIds";
        private const string DisableExecuteApiEndpointKey = "disableExecuteApiEndpoint";

        /// <summary>
        /// A list of VpcEndpoint identifiers against which to create Route 53 ALIASes for a REST Api. It is only supported for (PRIVATE) endpoint type.
        /// </summary>
        public IEnumerable<string> VpcEndpointIds { get; set; }

        /// <summary>
        /// Specifies whether clients can invoke your API by using the default execute-api endpoint. By default, clients can invoke your API with the default https://{api_id}.execute-api.{region}.amazonaws.com endpoint.
        /// </summary>
        public bool? DisableExecuteApiEndpoint { get; set; }

        /// <summary>
        /// A list of endpoint types of an API or its custom domain name. Valid values include:
        /// EDGE: For an edge-optimized API and its custom domain name.
        /// REGIONAL: For a regional API and its custom domain name.
        /// PRIVATE: For a private API.
        /// </summary>
        public IEnumerable<string> Types { get; set; }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var children = new OpenApiObject();

            if (Types != null && Types.Any())
            {
                var types = new OpenApiArray();
                types.AddRange(Types.Select(x => new OpenApiString(x)));

                children[TypesKey] = types;
            }

            if (VpcEndpointIds != null && VpcEndpointIds.Any())
            {
                var vpcIds = new OpenApiArray();
                vpcIds.AddRange(VpcEndpointIds.Select(x => new OpenApiString(x)));

                children[VpcEndpointIdsKey] = vpcIds;
            }

            if (DisableExecuteApiEndpoint.HasValue)
            {
                children[DisableExecuteApiEndpointKey] = new OpenApiBoolean(DisableExecuteApiEndpoint.Value);
            }

            return new Dictionary<string, IOpenApiAny>()
            {
                { EndpointConfigurationRootKey, children }
            };
        }
    }
}