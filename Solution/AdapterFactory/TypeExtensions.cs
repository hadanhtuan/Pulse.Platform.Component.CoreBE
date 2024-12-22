using System;
using System.Collections.Generic;

namespace AdapterFactory
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns the parent types, including base type and interfaces, of the given <paramref name="type"/>.
        /// </summary>
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