using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public static class Utils
    {
        public static IEnumerable<PropertyInfo> GetAllListProperties(object obj)
        {
            var currentType = obj.GetType();
            var listProps = new List<PropertyInfo>();

            while (currentType != null && currentType != typeof(object))
            {
                var props = currentType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

                listProps.AddRange(
                    from property in props
                    where property.PropertyType.IsGenericType
                    let generic = property.PropertyType.GetGenericTypeDefinition()
                    where generic == typeof(List<>)
                    let genericArgument = property.PropertyType.GetGenericArguments()[0]
                    where genericArgument == typeof(float)
                    select property
                );

                currentType = currentType.BaseType;
            }
            
            return listProps.OrderBy(p => p.Name);
        }
    }
}