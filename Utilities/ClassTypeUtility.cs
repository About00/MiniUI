using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniUI.Utilities
{
    public static class ClassTypeUtility
    {
        #region Static Methods

        public static List<Type[]> GetGenericArgumentTypes(Type reflectedType, Type genericInterfaceType)
        {
            return reflectedType.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == genericInterfaceType)
                .Select(i => i.GetGenericArguments())
                .ToList();
        }

        public static bool IsAssignableFromGenericInterface(Type reflectedType, Type genericInterfaceType)
        {
            return reflectedType.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == genericInterfaceType);
        }

        public static List<Type> GetImplementedTypesFrom(Type targetType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(targetType))
                .ToList();
        }

        public static List<Type> GetAssignableTypesFrom(Type targetType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass &&
                               targetType.IsAssignableFrom(type))
                .ToList();
        }

        #endregion
    }
}