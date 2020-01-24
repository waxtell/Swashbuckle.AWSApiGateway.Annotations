using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;

// ReSharper disable CommentTypo

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayBinaryMediaTypesOptions : AbstractOptions
    {
        private IEnumerable<string> _binaryMediaTypes;
        private const string BinaryMediaTypesKey = "x-amazon-apigateway-binary-media-types";

        public IEnumerable<string> BinaryMediaTypes
        {
            get => _binaryMediaTypes;
            set { _binaryMediaTypes = value; OnPropertyChanged(); }
        }

        internal override IDictionary<string, IOpenApiAny> ToDictionary()
        {
            var result = new Dictionary<string, IOpenApiAny>();

            if (HasPropertyChanged(nameof(BinaryMediaTypes)))
            {
                var binaryTypes = new OpenApiArray();
                binaryTypes.AddRange(BinaryMediaTypes.Select(x => new OpenApiString(x)));

                result[BinaryMediaTypesKey] = binaryTypes;
            }

            return result;
        }
    }
}