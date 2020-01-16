using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    internal static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Union<TKey, TValue>(this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second)
        {
            return
                first
                    .Concat(second)
                    .GroupBy(d => d.Key)
                    .ToDictionary(d => d.Key, d => d.Last().Value);
        }
    }
}