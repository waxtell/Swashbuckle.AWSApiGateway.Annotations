using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayEndpointConfigurationOptions : AbstractExtensionOptions
    {
        private const string EndpointConfigurationRootKey = "x-amazon-apigateway-endpoint-configuration";
        private const string TypesKey = "types";
        private const string VpcEndpointIdsKey = "vpcEndpointIds";

        /// <summary>
        /// A list of VpcEndpoint identifiers against which to create Route 53 ALIASes for a REST Api. It is only supported for (PRIVATE) endpoint type.
        /// </summary>
        public IEnumerable<string> VpcEndpointIds { get; set; }
        /// <summary>
        /// A list of endpoint types of an API or its custom domain name. Valid values include:
        /// EDGE: For an edge-optimized API and its custom domain name.
        /// REGIONAL: For a regional API and its custom domain name.
        /// PRIVATE: For a private API.
        /// </summary>
        public IEnumerable<string> Types { get; set; }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
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

            return new Dictionary<string, IOpenApiExtension>()
            {
                { EndpointConfigurationRootKey, children }
            };
        }
    }
}