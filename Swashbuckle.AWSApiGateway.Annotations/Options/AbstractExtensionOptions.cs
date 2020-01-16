using Microsoft.OpenApi.Interfaces;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public abstract class AbstractExtensionOptions
    {
        internal abstract IDictionary<string, IOpenApiExtension> ToDictionary();
    }
}