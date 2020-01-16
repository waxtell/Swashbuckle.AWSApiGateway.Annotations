using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayOptions : AbstractExtensionOptions
    {
        private readonly XAmazonApiGatewayKeySourceOptions _apiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions();
        private readonly XAmazonApiGatewayCORSOptions _corsOptions = new XAmazonApiGatewayCORSOptions();

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key.
        /// </summary>
        /// <param name="setupAction"></param>
        public void WithKeySource(Action<XAmazonApiGatewayKeySourceOptions> setupAction)
        {
            setupAction.Invoke(_apiKeySourceOptions);
        }

        /// <summary>
        /// Specifies the cross-origin resource sharing (CORS) configuration for an HTTP API. The extension applies to the root-level OpenAPI structure.
        /// </summary>
        /// <param name="setupAction"></param>
        public void WithCors(Action<XAmazonApiGatewayCORSOptions> setupAction)
        {
            setupAction.Invoke(_corsOptions);
        }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            return
                _apiKeySourceOptions
                    .ToDictionary()
                    .Union(_corsOptions.ToDictionary());
        }
    }
}