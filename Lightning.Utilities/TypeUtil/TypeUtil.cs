using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NuCore.Utilities
{  
    /// <summary>
    /// TypeUtil
    /// 
    /// October 18, 2021
    /// 
    /// Provides type utilities for Lightning 
    /// </summary>
    public static class TypeUtil
    {
        /// <summary>
        /// Returns a <see cref="List{T}"/> of types containing all derived types of the type <paramref name="BaseType"/>.
        /// </summary>
        /// <param name="BaseType">The base type to check.</param>
        /// <returns>A list of all derived types of the type <paramref name="BaseType"/>.</returns>
        public static List<Type> GetAllDerivedClasses(this Type BaseType)
        {
            Assembly TAssembly = BaseType.Assembly;

            // Use a fun little LINQ query
            List<Type> DerivedTypes = TAssembly.GetTypes().Where(CType => CType.IsSubclassOf(BaseType)).ToList();

            return DerivedTypes;
        }
    }
}
