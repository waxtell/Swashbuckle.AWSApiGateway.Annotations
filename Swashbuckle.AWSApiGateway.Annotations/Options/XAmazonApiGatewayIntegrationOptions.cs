﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
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
    }

    public class XAmazonApiGatewayIntegrationOptions : AbstractOptions, IXAmazonApiGatewayIntegrationOptions
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

        private PassthroughBehavior _passthroughBehavior;
        private string _httpMethod;
        private ContentHandling _contentHandling;
        private ConnectionType _connectionType;
        private string _connectionId;
        private string _credentials;
        private string _cacheNamespace;
        private int _timeoutInMillis;
        private IntegrationType _type;
        private string _uri;

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

        public IDictionary<string, string> RequestParameters { get; set; }

        /// <summary>
        /// Mapping templates for a request payload of specified MIME types.
        /// </summary>
        public IDictionary<string,string> RequestTemplates { get; set; }

        /// <summary>
        /// Defines the method's responses and specifies desired parameter mappings or payload mappings from integration responses to method responses.
        /// </summary>
        public Dictionary<string, IntegrationResponse> Responses { get; set; }

        /// <summary>
        /// The BaseUri will be used to generate the Uri value for operations where a Uri value is not explicitly provided.
        /// </summary>
        public string BaseUri { get; set; }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var children = new OpenApiObject();

            if (HasPropertyChanged(nameof(TimeoutInMillis)))
            {
                children[TimeoutInMillisKey] = new OpenApiInteger(TimeoutInMillis);
            }

            if (HasPropertyChanged(nameof(ConnectionType)))
            {
                children[ConnectionTypeKey] = new OpenApiString(Enum.GetName(typeof(ConnectionType), ConnectionType));
            }

            if (HasPropertyChanged(nameof(Type)))
            {
                children[TypeKey] = new OpenApiString(Enum.GetName(typeof(IntegrationType), Type));
            }

            if (HasPropertyChanged(nameof(Uri)))
            {
                children[UriKey] = new OpenApiString(Uri);
            }

            if (HasPropertyChanged(nameof(ConnectionId)))
            {
                children[ConnectionIdKey] = new OpenApiString(ConnectionId);
            }

            if (HasPropertyChanged(nameof(HttpMethod)))
            {
                children[HttpMethodKey] = new OpenApiString(HttpMethod);
            }

            if (HasPropertyChanged(nameof(Credentials)))
            {
                children[CredentialsKey] = new OpenApiString(Credentials);
            }

            if (HasPropertyChanged(nameof(CacheNamespace)))
            {
                children[CacheNamespaceKey] = new OpenApiString(CacheNamespace);
            }

            if(RequestParameters != null && RequestParameters.Any())
            {
                var requestParametersObject = new OpenApiObject();
                foreach (var item in RequestParameters)
                {
                    requestParametersObject[item.Key] = new OpenApiString(item.Value); 
                }
                children[RequestParametersKey] = requestParametersObject;
            }

            if (RequestTemplates != null && RequestTemplates.Any())
            {
                var requestTemplatesObject = new OpenApiObject();
                foreach (var item in RequestTemplates)
                {
                    requestTemplatesObject[item.Key] = new OpenApiString(item.Value);
                }

                children[RequestTemplatesKey] = requestTemplatesObject;
            }

            if (Responses != null && Responses.Any())
            {
                var responsesContainer = new OpenApiObject();

                foreach (var response in Responses)
                {
                    var responseContainer = new OpenApiObject();
                    foreach (var item in response.Value.ToDictionary())
                    {
                        responseContainer[item.Key] = item.Value;
                    }

                    responsesContainer[response.Key] = responseContainer;
                }

                children["responses"] = responsesContainer;
            }

            if (HasPropertyChanged(nameof(ContentHandling)))
            {
                children[ContentHandlingKey] = new OpenApiString(Enum.GetName(typeof(ContentHandling), ContentHandling));
            }

            if (HasPropertyChanged(nameof(PassthroughBehavior)))
            {
                children[PassthroughBehaviorKey] = new OpenApiString(Enum.GetName(typeof(PassthroughBehavior), PassthroughBehavior));
            }

            return new Dictionary<string, IOpenApiAny>()
            {
                { IntegrationRootKey, children }
            };
        }
    }
}
