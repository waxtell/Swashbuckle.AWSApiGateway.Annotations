using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public class IntegrationResponse : AbstractOptions
    {
        private const string StatusCodeKey = "statusCode";
        private const string ResponseTemplatesKey = "responseTemplates";
        private const string ResponseParametersKey = "responseParameters";

        public string StatusCode { get; set; }
        public Dictionary<string,string> ResponseTemplates { get; set; }
        public Dictionary<string,string> ResponseParameters { get; set; }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var responseObject = new OpenApiObject
            {
                [StatusCodeKey] = new OpenApiString(StatusCode)
            };
            
            if (ResponseTemplates != null && ResponseTemplates.Any())
            {
                var templatesContainer = new OpenApiObject();

                foreach (var template in ResponseTemplates)
                {
                    templatesContainer[template.Key] = new OpenApiString(template.Value);
                }

                responseObject[ResponseTemplatesKey] = templatesContainer;
            }

            if (ResponseParameters != null && ResponseParameters.Any())
            {
                var parametersContainer = new OpenApiObject();

                foreach (var parameter in ResponseParameters)
                {
                    parametersContainer[parameter.Key] = new OpenApiString(parameter.Value);
                }

                responseObject[ResponseParametersKey] = parametersContainer;
            }

            return responseObject;
        }
    }
}