using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;

// ReSharper disable CommentTypo

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayBinaryMediaTypesOptions : AbstractOptions
    {
        private const string BinaryMediaTypesKey = "x-amazon-apigateway-binary-media-types";

        public IEnumerable<string> BinaryMediaTypes { get; set; }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var result = new Dictionary<string, IOpenApiAny>();

            if (BinaryMediaTypes != null && BinaryMediaTypes.Any())
            {
                var binaryTypes = new OpenApiArray();
                binaryTypes.AddRange(BinaryMediaTypes.Select(x => new OpenApiString(x)));

                result[BinaryMediaTypesKey] = binaryTypes;
            }

            return result;
        }
    }
}