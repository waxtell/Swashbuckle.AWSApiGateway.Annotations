using System;
using System.Collections.Generic;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    public abstract class AbstractTrackingAttribute : Attribute
    {
        internal abstract IEnumerable<string> GetChangedProperties();
    }
}
