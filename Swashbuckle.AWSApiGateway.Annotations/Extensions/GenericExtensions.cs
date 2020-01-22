using System.Linq;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    internal static class GenericExtensions
    {
        public static void Merge<T>(this T target, T source) where T 
            : AbstractExtensionOptions
        {
            var t = typeof(T);
            var changedProperties = source
                                        .GetChangedProperties()
                                        .ToList();

            var properties = t.GetProperties().Where(prop => changedProperties.Contains(prop.Name));

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);

                if (value != null)
                {
                    prop.SetValue(target, value, null);
                }
            }
        }

        public static void Merge<T,TI,TAttr>(this T target, TAttr source) 
            where T : TI
            where TAttr: AbstractTrackingAttribute, TI
        {
            var t = typeof(TI);
            var changedProperties = source
                                        .GetChangedProperties()
                                        .ToList();

            var properties = t.GetProperties().Where(prop => changedProperties.Contains(prop.Name));

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);

                if (value != null)
                {
                    prop.SetValue(target, value, null);
                }
            }
        }
    }
}
