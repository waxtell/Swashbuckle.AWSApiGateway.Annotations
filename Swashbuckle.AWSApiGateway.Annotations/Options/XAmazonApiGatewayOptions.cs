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

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key.
        /// </summary>
        /// <param name="setupAction"></param>
        public void WithKeySource(Action<XAmazonApiGatewayKeySourceOptions> setupAction)
        {
            _apiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions();

            setupAction.Invoke(_apiKeySourceOptions);
        }

        /// <summary>
        /// Specifies the cross-origin resource sharing (CORS) configuration for an HTTP API. The extension applies to the root-level OpenAPI structure.
        /// </summary>
        /// <param name="setupAction"></param>
        public void WithCors(Action<XAmazonApiGatewayCORSOptions> setupAction)
        {
            _corsOptions = new XAmazonApiGatewayCORSOptions();

            setupAction.Invoke(_corsOptions);
        }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            return
                (_apiKeySourceOptions?.ToDictionary() ?? new Dictionary<string,IOpenApiExtension>())
                    .Union(_corsOptions?.ToDictionary());
        }
    }
}