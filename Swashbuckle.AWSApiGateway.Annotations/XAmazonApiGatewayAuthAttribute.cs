using System.Collections.Generic;
using Swashbuckle.AWSApiGateway.Annotations.Enums;
using Swashbuckle.AWSApiGateway.Annotations.Options;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayAuthAttribute : AbstractTrackingAttribute, IXAmazonApiGatewayAuthOptions
    {
        private readonly XAmazonApiGatewayAuthOptions _ixAmazonApiGatewayAuthOptionsImplementation = new XAmazonApiGatewayAuthOptions();

        public AuthType AuthType
        {
            get => _ixAmazonApiGatewayAuthOptionsImplementation.AuthType;
            set => _ixAmazonApiGatewayAuthOptionsImplementation.AuthType = value;
        }

        internal override IEnumerable<string> GetChangedProperties()
        {
            return
                _ixAmazonApiGatewayAuthOptionsImplementation
                    .GetChangedProperties();
        }
    }
}