using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayOptions : AbstractOptions
    {
        private XAmazonApiGatewayKeySourceOptions _apiKeySourceOptions;
        private XAmazonApiGatewayCORSOptions _corsOptions;
        private XAmazonApiGatewayBinaryMediaTypesOptions _binaryMediaTypesOptions;
        private XAmazonApiGatewayRequestValidators _amazonApiGatewayRequestValidators;
        private XAmazonApiGatewayRequestValidatorOptions _requestValidatorOptions;

        /// <summary>
        /// Specify the source to receive an API key to throttle API methods that require a key.
        /// </summary>
        /// <param name="apiKeySource">HEADER or AUTHORIZER</param>
        public XAmazonApiGatewayOptions WithKeySource(ApiKeySource apiKeySource)
        {
            _apiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions
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

        /// <summary>
        /// Specifies a request validator, by referencing a request_validator_name of the x-amazon-apigateway-request-validators Object map, to enable request validation on the containing API or a method. The value of this extension is a JSON string.
        /// This extension can be specified at the API level or at the method level.The API-level validator applies to all of the methods unless it is overridden by the method-level validator.
        /// </summary>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public XAmazonApiGatewayOptions WithRequestValidator(Action<XAmazonApiGatewayRequestValidatorOptions> setupAction)
        {
            _requestValidatorOptions = new XAmazonApiGatewayRequestValidatorOptions();

            setupAction.Invoke(_requestValidatorOptions);

            return this;
        }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            return
                (_apiKeySourceOptions?.ToDictionary() ?? new Dictionary<string, IOpenApiAny>())
                    .Union(_corsOptions?.ToDictionary())
                    .Union(_binaryMediaTypesOptions?.ToDictionary())
                    .Union(_amazonApiGatewayRequestValidators?.ToDictionary())
                    .Union(_requestValidatorOptions?.ToDictionary())
                ;
        }
    }
}