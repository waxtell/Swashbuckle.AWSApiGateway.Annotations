using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AWSApiGateway.Annotations.Options;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayIntegrationAttribute : Attribute, IXAmazonApiGatewayIntegrationOptions
    {
        private readonly XAmazonApiGatewayIntegrationOptions _ixAmazonApiGatewayOperationOptionsImplementation = new XAmazonApiGatewayIntegrationOptions();

        public PassthroughBehavior PassthroughBehavior
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.PassthroughBehavior;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.PassthroughBehavior = value;
        }

        public string HttpMethod
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.HttpMethod;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.HttpMethod = value;
        }

        public ContentHandling ContentHandling
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.ContentHandling;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.ContentHandling = value;
        }

        public ConnectionType ConnectionType
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.ConnectionType;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.ConnectionType = value;
        }

        public string ConnectionId
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.ConnectionId;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.ConnectionId = value;
        }

        public string Credentials
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.Credentials;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.Credentials = value;
        }

        public string CacheNamespace
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.CacheNamespace;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.CacheNamespace = value;
        }

        public int TimeoutInMillis
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.TimeoutInMillis;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.TimeoutInMillis = value;
        }

        public IntegrationType Type
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.Type;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.Type = value;
        }

        public string Uri
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.Uri;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.Uri = value;
        }

        public string RequestParameters
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.RequestParameters;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.RequestParameters = value;
        }

        public string RequestTemplates
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.RequestTemplates;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.RequestTemplates = value;
        }

        public string Responses
        {
            get => _ixAmazonApiGatewayOperationOptionsImplementation.Responses;
            set => _ixAmazonApiGatewayOperationOptionsImplementation.Responses = value;
        }

        public IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            return 
                _ixAmazonApiGatewayOperationOptionsImplementation
                    .ToDictionary();
        }
    }
}