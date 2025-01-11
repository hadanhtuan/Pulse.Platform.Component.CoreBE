using System;
using System.Collections.Generic;

namespace Domain.AdapterFactory
{
    public static class TypeExtensions
    {
        
        /// Returns the parent types, including base type and interfaces, of the given <paramref name="type"/>.
        
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            if (type == null)
            {
                yield break;
            }

            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }
    }
}