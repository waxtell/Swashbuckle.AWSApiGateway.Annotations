using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayOptions : AbstractExtensionOptions
    {
        private XAmazonApiGatewayKeySourceOptions _apiKeySourceOptions;
        private XAmazonApiGatewayCORSOptions _corsOptions;
        private XAmazonApiGatewayBinaryMediaTypesOptions _binaryMediaTypesOptions;
        private XAmazonApiGatewayRequestValidators _amazonApiGatewayRequestValidators;

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key.
        /// </summary>
        /// <param name="setupAction"></param>
        public XAmazonApiGatewayOptions WithKeySource(Action<XAmazonApiGatewayKeySourceOptions> setupAction)
        {
            _apiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions();

            setupAction.Invoke(_apiKeySourceOptions);

            return this;
        }

        /// <summary>
        /// Specifies the cross-origin resource sharing (CORS) configuration for an HTTP API. The extension applies to the root-level OpenAPI structure.
        /// </summary>
        /// <param name="setupAction"></param>
        public XAmazonApiGatewayOptions WithCors(Action<XAmazonApiGatewayCORSOptions> setupAction)
        {
            _corsOptions = new XAmazonApiGatewayCORSOptions();

            setupAction.Invoke(_corsOptions);

            return this;
        }

        public XAmazonApiGatewayOptions WithBinaryMediaTypes(Action<XAmazonApiGatewayBinaryMediaTypesOptions> setupAction)
        {
            _binaryMediaTypesOptions = new XAmazonApiGatewayBinaryMediaTypesOptions();

            setupAction.Invoke(_binaryMediaTypesOptions);

            return this;
        }

        public XAmazonApiGatewayOptions WithRequestValidators(Action<XAmazonApiGatewayRequestValidators> setupAction)
        {
            _amazonApiGatewayRequestValidators = new XAmazonApiGatewayRequestValidators();

            setupAction.Invoke(_amazonApiGatewayRequestValidators);

            return this;
        }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            return
                (_apiKeySourceOptions?.ToDictionary() ?? new Dictionary<string,IOpenApiExtension>())
                    .Union(_corsOptions?.ToDictionary())
                    .Union(_binaryMediaTypesOptions?.ToDictionary())
                    .Union(_amazonApiGatewayRequestValidators?.ToDictionary())
                ;
        }
    }
}