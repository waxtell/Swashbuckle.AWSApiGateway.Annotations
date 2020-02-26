using System;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class XAmazonApiGatewayIntegrationRequestParameterAttribute : Attribute
    {
        public string MethodRequestParameter { get; }
        public string IntegrationRequestParameter { get; }

        /// <summary>
        /// Specifies mappings from method request parameters to integration request parameters. Supported request parameters are querystring, path, header, and body.
        /// </summary>
        public XAmazonApiGatewayIntegrationRequestParameterAttribute(string integrationRequestParameter, string methodRequestParameter)
        {
            MethodRequestParameter = methodRequestParameter;
            IntegrationRequestParameter = integrationRequestParameter;
        }
    }
}