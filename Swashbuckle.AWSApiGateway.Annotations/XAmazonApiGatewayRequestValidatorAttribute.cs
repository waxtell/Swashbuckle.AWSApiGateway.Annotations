using System.Collections.Generic;
using Swashbuckle.AWSApiGateway.Annotations.Options;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayRequestValidatorAttribute : AbstractTrackingAttribute, IXAmazonApiGatewayRequestValidatorOptions
    {
        private readonly XAmazonApiGatewayRequestValidatorOptions _ixAmazonApiGatewayRequestValidatorOptionsImplementation = new XAmazonApiGatewayRequestValidatorOptions();

        public string RequestValidator
        {
            get => _ixAmazonApiGatewayRequestValidatorOptionsImplementation.RequestValidator;
            set => _ixAmazonApiGatewayRequestValidatorOptionsImplementation.RequestValidator = value;
        }

        internal override IEnumerable<string> GetChangedProperties()
        {
            return
                _ixAmazonApiGatewayRequestValidatorOptionsImplementation
                    .GetChangedProperties();
        }
    }
}