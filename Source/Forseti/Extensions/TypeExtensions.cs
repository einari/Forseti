using System;
using System.Reflection;

namespace Forseti.Extensions
{
	/// <summary>
	/// Provides a set of methods for working with <see cref="Type">types</see>
	/// </summary>
	public static class TypeExtensions
	{
#pragma warning disable 1591 // Xml Comments
        static ITypeInfo GetTypeInfo(Type type)
        {
            var typeInfoType = typeof(TypeInfo<>).MakeGenericType(type);
            return typeInfoType.GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as ITypeInfo;
        }
#pragma warning restore 1591 // Xml Comments


        /// <summary>
        /// Check if a type has a default constructor that does not take any arguments
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>true if it has a default constructor, false if not</returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            return GetTypeInfo(type).HasDefaultConstructor;
        }

		/// <summary>
		/// Check if a type implements a specific interface
		/// </summary>
		/// <typeparam name="T">Interface to check for</typeparam>
		/// <param name="type">Type to check</param>
		/// <returns>True if the type implements the interface, false if not</returns>
		public static bool HasInterface<T>(this Type type)
		{
		    var hasInterface = type.HasInterface(typeof (T));
			return hasInterface;
		}

        /// <summary>
        /// Check if a type implements a specific interface
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <param name="interfaceType">Interface to check for</param>
        /// <returns>True if the type implements the interface, false if not</returns>
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            var hasInterface = type.GetInterface(interfaceType.Name, false) != null;
            return hasInterface;
        }

	}
}