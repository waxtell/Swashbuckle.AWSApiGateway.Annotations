using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public interface IXAmazonApiGatewayIntegrationOptions
    {
        /// <summary>
        /// Specifies how a request payload of unmapped content type is passed through the integration request without modification. Supported values are when_no_templates, when_no_match, and never.
        /// </summary>
        PassthroughBehavior PassthroughBehavior { get; set; }

        /// <summary>
        /// The HTTP method used in the integration request. For Lambda function invocations, the value must be POST.
        /// </summary>
        string HttpMethod { get; set; }

        /// <summary>
        /// Request payload encoding conversion types. Valid values are 1) CONVERT_TO_TEXT, for converting a binary payload into a Base64-encoded string or converting a text payload into a utf-8-encoded string or passing through the text payload natively without modification, and 2) CONVERT_TO_BINARY, for converting a text payload into Base64-decoded blob or passing through a binary payload natively without modification.
        /// </summary>
        ContentHandling ContentHandling { get; set; }

        /// <summary>
        /// The integration connection type. The valid value is "VPC_LINK" for private integration or "INTERNET", otherwise.
        /// </summary>
        ConnectionType ConnectionType { get; set; }

        /// <summary>
        /// The ID of a VpcLink for the private integration.
        /// </summary>
        string ConnectionId { get; set; }

        /// <summary>
        /// For AWS IAM role-based credentials, specify the ARN of an appropriate IAM role. If unspecified, credentials will default to resource-based permissions that must be added manually to allow the API to access the resource. For more information, see Granting Permissions Using a Resource Policy. Note: when using IAM credentials, please ensure that AWS STS regional endpoints are enabled for the region where this API is deployed for best performance.
        /// </summary>
        string Credentials { get; set; }

        /// <summary>
        /// An API-specific tag group of related cached parameters.
        /// </summary>
        string CacheNamespace { get; set; }

        /// <summary>
        /// Integration timeouts between 50 ms and 29,000 ms.
        /// </summary>
        int TimeoutInMillis { get; set; }

        /// <summary>
        /// The type of integration with the specified backend. The valid value is
        /// http or http_proxy: for integration with an HTTP backend;;
        /// aws_proxy: for integration with AWS Lambda functions;
        /// aws; for integration with AWS Lambda functions or other AWS services, such as Amazon DynamoDB, Amazon Simple Notification Service or Amazon Simple Queue Service;
        /// mock: for integration with API Gateway without invoking any backend.
        /// </summary>
        IntegrationType Type { get; set; }

        /// <summary>
        /// The endpoint URI of the backend. For integrations of the aws type, this is an ARN value. For the HTTP integration, this is the URL of the HTTP endpoint including the https or http scheme.
        /// </summary>
        string Uri { get; set; }

        /// <summary>
        /// Specifies mappings from method request parameters to integration request parameters. Supported request parameters are querystring, path, header, and body.
        /// </summary>
        string RequestParameters { get; set; }

        /// <summary>
        /// Mapping templates for a request payload of specified MIME types.
        /// </summary>
        string RequestTemplates { get; set; }

        /// <summary>
        /// Defines the method's responses and specifies desired parameter mappings or payload mappings from integration responses to method responses.
        /// </summary>
        string Responses { get; set; }
    }

    public class XAmazonApiGatewayIntegrationOptions : AbstractExtensionOptions, IXAmazonApiGatewayIntegrationOptions
    {
        private const string IntegrationRootKey = "x-amazon-apigateway-integration";
        private const string UriKey = "uri";
        private const string TypeKey = "type";
        private const string TimeoutInMillisKey = "timeoutInMillis";
        private const string CacheNamespaceKey = "cacheNamespace";
        private const string CredentialsKey = "credentials";
        private const string ConnectionIdKey = "connectionId";
        private const string ConnectionTypeKey = "connectionType";
        private const string ContentHandlingKey = "contentHandling";
        private const string HttpMethodKey = "httpMethod";
        private const string PassthroughBehaviorKey = "passthroughBehavior";
        private const string RequestParametersKey = "requestParameters";
        private const string RequestTemplatesKey = "requestTemplates";
        private const string ResponsesKey = "responses";

        public PassthroughBehavior PassthroughBehavior { get; set; } = PassthroughBehavior.UNDEFINED;
        public string HttpMethod { get; set; }
        public ContentHandling ContentHandling { get; set; } = ContentHandling.UNDEFINED;
        public ConnectionType ConnectionType { get; set; } = ConnectionType.INTERNET;
        public string ConnectionId { get; set; }
        public string Credentials { get; set; }
        public string CacheNamespace { get; set; }
        public int TimeoutInMillis { get; set; } = 29000;
        public IntegrationType Type { get; set; } = IntegrationType.aws;
        public string Uri { get; set; }
        public string RequestParameters { get; set; }
        public string RequestTemplates { get; set; }
        public string Responses { get; set; }
        /// <summary>
        /// The BaseUri will be used to generate the Uri value for operations where a Uri value is not explicitly provided.
        /// </summary>
        public string BaseUri { get; set; }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            var children = new OpenApiObject
            {
                [TimeoutInMillisKey] = new OpenApiInteger(TimeoutInMillis),
                [ConnectionTypeKey] = new OpenApiString(Enum.GetName(typeof(ConnectionType), ConnectionType))
            };

            if (Type != IntegrationType.UNDEFINED)
            {
                children[TypeKey] = new OpenApiString(Enum.GetName(typeof(IntegrationType), Type));
            }

            if (!string.IsNullOrEmpty(Uri))
            {
                children[UriKey] = new OpenApiString(Uri);
            }

            if (!string.IsNullOrEmpty(ConnectionId))
            {
                children[ConnectionIdKey] = new OpenApiString(ConnectionId);
            }

            if (!string.IsNullOrEmpty(HttpMethod))
            {
                children[HttpMethodKey] = new OpenApiString(HttpMethod);
            }

            if (!string.IsNullOrEmpty(Credentials))
            {
                children[CredentialsKey] = new OpenApiString(Credentials);
            }

            if (!string.IsNullOrEmpty(CacheNamespace))
            {
                children[CacheNamespaceKey] = new OpenApiString(CacheNamespace);
            }

            if (!string.IsNullOrEmpty(RequestParameters))
            {
                children[RequestParametersKey] = new OpenApiString(RequestParameters);
            }

            if (!string.IsNullOrEmpty(RequestTemplates))
            {
                children[RequestTemplatesKey] = new OpenApiString(RequestTemplates);
            }

            if (!string.IsNullOrEmpty(Responses))
            {
                children[ResponsesKey] = new OpenApiString(Responses);
            }

            if (ContentHandling != ContentHandling.UNDEFINED)
            {
                children[ContentHandlingKey] = new OpenApiString(Enum.GetName(typeof(ContentHandling), ContentHandling));
            }

            if (PassthroughBehavior != PassthroughBehavior.UNDEFINED)
            {
                children[PassthroughBehaviorKey] = new OpenApiString(Enum.GetName(typeof(PassthroughBehavior), PassthroughBehavior));
            }

            return new Dictionary<string, IOpenApiExtension>()
            {
                { IntegrationRootKey, children }
            };
        }
    }
}
