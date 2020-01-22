using Microsoft.OpenApi.Interfaces;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public abstract class AbstractExtensionOptions
    {
        private readonly List<string> _changedProperties = new List<string>();

        internal abstract IDictionary<string, IOpenApiExtension> ToDictionary();

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