using System;
using Swashbuckle.AWSApiGateway.Annotations.Enums;
using Swashbuckle.AWSApiGateway.Annotations.Options;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayAuthAttribute : Attribute, IXAmazonApiGatewayAuthOptions
    {
        private readonly IXAmazonApiGatewayAuthOptions _ixAmazonApiGatewayAuthOptionsImplementation = new XAmazonApiGatewayAuthOptions();

        public AuthType AuthType
        {
            get => _ixAmazonApiGatewayAuthOptionsImplementation.AuthType;
            set => _ixAmazonApiGatewayAuthOptionsImplementation.AuthType = value;
        }
    }
}