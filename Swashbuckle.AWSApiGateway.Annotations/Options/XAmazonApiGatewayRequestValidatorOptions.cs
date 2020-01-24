using System.Collections.Generic;
using Microsoft.OpenApi.Any;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public interface IXAmazonApiGatewayRequestValidatorOptions
    {
        /// <summary>
        /// Specifies a request validator, by referencing a request_validator_name of the x-amazon-apigateway-request-validators Object map, to enable request validation on the containing API or a method.
        /// </summary>
        string RequestValidator { get; set; }
    }

    public class XAmazonApiGatewayRequestValidatorOptions : AbstractOptions, IXAmazonApiGatewayRequestValidatorOptions
    {
        private const string RequestValidatorRootKey = "x-amazon-apigateway-request-validator";

        private string _requestValidator;

        public string RequestValidator
        {
            get => _requestValidator;
            set { _requestValidator = value; OnPropertyChanged(); }
        }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var result = new Dictionary<string, IOpenApiAny>();

            if (HasPropertyChanged(nameof(RequestValidator)))
            {
                result[RequestValidatorRootKey] = new OpenApiString(RequestValidator);
            }

            return result;
        }
    }
}
