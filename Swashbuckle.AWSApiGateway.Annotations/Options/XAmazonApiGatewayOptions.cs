using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Any;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayOptions : AbstractOptions
    {
        internal XAmazonApiGatewayKeySourceOptions ApiKeySourceOptions { get; private set; }
        internal XAmazonApiGatewayCORSOptions CorsOptions { get; private set; }
        internal XAmazonApiGatewayBinaryMediaTypesOptions BinaryMediaTypesOptions { get; private set; }
        internal XAmazonApiGatewayRequestValidators AmazonApiGatewayRequestValidators { get; private set; }
        internal XAmazonApiGatewayRequestValidatorOptions RequestValidatorOptions { get; private set; }

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key.
        /// </summary>
        /// <param name="apiKeySource">HEADER or AUTHORIZER</param>
        public XAmazonApiGatewayOptions WithKeySource(ApiKeySource apiKeySource)
        {
            ApiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions
            {
                ApiKeySource = apiKeySource
            };

            return this;
        }

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key.
        /// </summary>
        /// <param name="setupAction"></param>
        public XAmazonApiGatewayOptions WithKeySource(Action<XAmazonApiGatewayKeySourceOptions> setupAction)
        {
            ApiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions();

            setupAction.Invoke(ApiKeySourceOptions);

            return this;
        }

        /// <summary>
        /// Specifies the cross-origin resource sharing (CORS) configuration for an HTTP API. The extension applies to the root-level OpenAPI structure.
        /// </summary>
        /// <param name="setupAction"></param>
        public XAmazonApiGatewayOptions WithCors(Action<XAmazonApiGatewayCORSOptions> setupAction)
        {
            CorsOptions = new XAmazonApiGatewayCORSOptions();

            setupAction.Invoke(CorsOptions);

            return this;
        }

        /// <summary>
        /// Specifies the cross-origin resource sharing (CORS) configuration for an HTTP API. The extension applies to the root-level OpenAPI structure.
        /// </summary>
        /// <param name="policy">The CorsPolicy from your Api</param>
        /// <param name="setupAction"></param>/// 
        public XAmazonApiGatewayOptions WithCors(CorsPolicy policy, Action<XAmazonApiGatewayCORSOptions> setupAction)
        {
            CorsOptions = XAmazonApiGatewayCORSOptionsFactory.FromCorsPolicy(policy);

            setupAction.Invoke(CorsOptions);

            return this;
        }

        public XAmazonApiGatewayOptions WithBinaryMediaTypes(Action<XAmazonApiGatewayBinaryMediaTypesOptions> setupAction)
        {
            BinaryMediaTypesOptions = new XAmazonApiGatewayBinaryMediaTypesOptions();

            setupAction.Invoke(BinaryMediaTypesOptions);

            return this;
        }

        public XAmazonApiGatewayOptions WithRequestValidators(Action<XAmazonApiGatewayRequestValidators> setupAction)
        {
            AmazonApiGatewayRequestValidators = new XAmazonApiGatewayRequestValidators();

            setupAction.Invoke(AmazonApiGatewayRequestValidators);

            return this;
        }

        /// <summary>
        /// Specifies a request validator, by referencing a request_validator_name of the x-amazon-apigateway-request-validators Object map, to enable request validation on the containing API or a method. The value of this extension is a JSON string.
        /// This extension can be specified at the API level or at the method level.The API-level validator applies to all of the methods unless it is overridden by the method-level validator.
        /// </summary>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public XAmazonApiGatewayOptions WithRequestValidator(Action<XAmazonApiGatewayRequestValidatorOptions> setupAction)
        {
            RequestValidatorOptions = new XAmazonApiGatewayRequestValidatorOptions();

            setupAction.Invoke(RequestValidatorOptions);

            return this;
        }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            return
                (ApiKeySourceOptions?.ToDictionary() ?? new Dictionary<string, IOpenApiAny>())
                    .Union(CorsOptions?.ToDictionary())
                    .Union(BinaryMediaTypesOptions?.ToDictionary())
                    .Union(AmazonApiGatewayRequestValidators?.ToDictionary())
                    .Union(RequestValidatorOptions?.ToDictionary())
                ;
        }
    }
}