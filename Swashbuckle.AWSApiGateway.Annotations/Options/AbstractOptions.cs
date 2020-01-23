using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public abstract class AbstractOptions
    {
        private readonly List<string> _changedProperties = new List<string>();

        internal abstract IDictionary<string, IOpenApiAny> ToDictionary();

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _changedProperties.Add(propertyName);
        }

        internal IEnumerable<string> GetChangedProperties()
        {
            return
                _changedProperties
                    .Where(propertyName => !string.IsNullOrWhiteSpace(propertyName))
                    .Distinct();
        }
    }
}