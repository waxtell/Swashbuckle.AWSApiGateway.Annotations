using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
// ReSharper disable UnusedMemberInSuper.Global

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

        private PassthroughBehavior _passthroughBehavior;
        private string _httpMethod;
        private ContentHandling _contentHandling;
        private ConnectionType _connectionType = ConnectionType.INTERNET;
        private string _connectionId;
        private string _credentials;
        private string _cacheNamespace;
        private int _timeoutInMillis = 29000;
        private IntegrationType _type = IntegrationType.aws;
        private string _uri;
        private string _requestParameters;
        private string _requestTemplates;
        private string _responses;

        public PassthroughBehavior PassthroughBehavior
        {
            get => _passthroughBehavior;
            set { _passthroughBehavior = value; OnPropertyChanged(); }
        }

        public string HttpMethod
        {
            get => _httpMethod;
            set { _httpMethod = value; OnPropertyChanged(); }
        }

        public ContentHandling ContentHandling
        {
            get => _contentHandling;
            set { _contentHandling = value; OnPropertyChanged(); }
        }

        public ConnectionType ConnectionType
        {
            get => _connectionType;
            set { _connectionType = value; OnPropertyChanged(); }
        }

        public string ConnectionId
        {
            get => _connectionId;
            set { _connectionId = value; OnPropertyChanged(); }
        }

        public string Credentials
        {
            get => _credentials;
            set { _credentials = value; OnPropertyChanged(); }
        }

        public string CacheNamespace
        {
            get => _cacheNamespace;
            set { _cacheNamespace = value; OnPropertyChanged(); }
        }

        public int TimeoutInMillis
        {
            get => _timeoutInMillis;
            set { _timeoutInMillis = value; OnPropertyChanged(); }
        }

        public IntegrationType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public string Uri
        {
            get => _uri;
            set { _uri = value; OnPropertyChanged(); }
        }

        public string RequestParameters
        {
            get => _requestParameters;
            set { _requestParameters = value; OnPropertyChanged(); }
        }

        public string RequestTemplates
        {
            get => _requestTemplates;
            set { _requestTemplates = value; OnPropertyChanged(); }
        }

        public string Responses
        {
            get => _responses;
            set { _responses = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// The BaseUri will be used to generate the Uri value for operations where a Uri value is not explicitly provided.
        /// </summary>
        public string BaseUri { get; set; }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            var changedProperties = GetChangedProperties().ToList();

            var children = new OpenApiObject();

            if (changedProperties.Contains(nameof(TimeoutInMillis)))
            {
                children[TimeoutInMillisKey] = new OpenApiInteger(TimeoutInMillis);
            }

            if (changedProperties.Contains(nameof(ConnectionType)))
            {
                children[ConnectionTypeKey] = new OpenApiString(Enum.GetName(typeof(ConnectionType), ConnectionType));
            }

            if (changedProperties.Contains(nameof(Type)))
            {
                children[TypeKey] = new OpenApiString(Enum.GetName(typeof(IntegrationType), Type));
            }

            if (changedProperties.Contains(nameof(Uri)))
            {
                children[UriKey] = new OpenApiString(Uri);
            }

            if (changedProperties.Contains(nameof(ConnectionId)))
            {
                children[ConnectionIdKey] = new OpenApiString(ConnectionId);
            }

            if (changedProperties.Contains(nameof(HttpMethod)))
            {
                children[HttpMethodKey] = new OpenApiString(HttpMethod);
            }

            if (changedProperties.Contains(nameof(Credentials)))
            {
                children[CredentialsKey] = new OpenApiString(Credentials);
            }

            if (changedProperties.Contains(nameof(CacheNamespace)))
            {
                children[CacheNamespaceKey] = new OpenApiString(CacheNamespace);
            }

            if (changedProperties.Contains(nameof(RequestParameters)))
            {
                children[RequestParametersKey] = new OpenApiString(RequestParameters);
            }

            if (changedProperties.Contains(nameof(RequestTemplates)))
            {
                children[RequestTemplatesKey] = new OpenApiString(RequestTemplates);
            }

            if (changedProperties.Contains(nameof(Responses)))
            {
                children[ResponsesKey] = new OpenApiString(Responses);
            }

            if (changedProperties.Contains(nameof(ContentHandling)))
            {
                children[ContentHandlingKey] = new OpenApiString(Enum.GetName(typeof(ContentHandling), ContentHandling));
            }

            if (changedProperties.Contains(nameof(PassthroughBehavior)))
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
