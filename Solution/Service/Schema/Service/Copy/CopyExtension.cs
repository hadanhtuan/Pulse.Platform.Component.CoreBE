using System.Reflection;

namespace Service.Schema.Service.Copy;

internal static class CopyExtension
{
    
    /// Copy the value of all writable properties on type T from source
    /// to target, excluding those with name in the ignoredProperties.
    
    /// <typeparam name="T">type of copied objecte</typeparam>
    /// <param name="source">source object</param>
    /// <param name="target">target object</param>
    /// <param name="ignoredProperties">names of properties not to be copied</param>
    /// <returns>the modified target object</returns>
    internal static T CopyPropertiesInto<T>(this T source, T target, List<string> ignoredProperties)
    {
        var properties = source.GetType().GetProperties()
            .Where(p => p.CanWrite && !ignoredProperties.Contains(p.Name))
            .ToList();

        foreach (var prop in properties)
        {
            PropertyInfo propertyInfo = target.GetType().GetProperty(prop.Name)!;
            propertyInfo.SetValue(target, prop.GetValue(source));
        }

        return target;
    }
}