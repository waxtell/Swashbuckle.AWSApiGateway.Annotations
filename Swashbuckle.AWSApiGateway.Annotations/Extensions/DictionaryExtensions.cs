using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    internal static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Union<TKey, TValue>(this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second)
        {
            if (second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            return
                first
                    .Concat(second)
                    .GroupBy(d => d.Key)
                    .ToDictionary(d => d.Key, d => d.Last().Value);
        }
    }
}