using Microsoft.OpenApi.Any;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public abstract class AbstractItem
    {
        internal abstract (string, IOpenApiAny) ToDictionaryItem();
    }

    public class RequestValidator : AbstractItem
    {
        private const string ValidateRequestBodyKey = "validateRequestBody";
        private const string ValidateRequestParametersKey = "validateRequestParameters";

        private readonly string _name;

        /// <summary>
        /// Specifies whether to validate the request body (true) or not (false).
        /// </summary>
        public bool ValidateRequestBody { get; set; } = false;

        /// <summary>
        /// Specifies whether to validate the required request parameters (true) or not (false).
        /// </summary>
        public bool ValidateRequestParameters { get; set; } = false;

        public RequestValidator(string name)
        {
            _name = name;
        }

        internal override (string, IOpenApiAny) ToDictionaryItem()
        {
            return
            (
                _name,
                new OpenApiObject
                {
                    {ValidateRequestBodyKey, new OpenApiBoolean(ValidateRequestBody)},
                    {ValidateRequestParametersKey, new OpenApiBoolean(ValidateRequestParameters)}
                }
            );
        }
    }
}
