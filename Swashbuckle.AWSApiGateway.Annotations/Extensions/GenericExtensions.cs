using System.Linq;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    internal static class GenericExtensions
    {
        public static void Merge<T>(this T target, T source)
        {
            var t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                prop.SetValue(target, value, null);
            }
        }
    }
}
