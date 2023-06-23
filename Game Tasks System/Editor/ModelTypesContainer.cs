using System;
using System.Reflection;
using System.Collections.Generic;

namespace GameTasks
{
    public static class ModelTypesContainer
    {
        private static readonly Dictionary<Type, ComponentModelAttribute> _componentModelTypes = new();

        public static IEnumerable<KeyValuePair<Type, ComponentModelAttribute>> GetComponentModelTypes() => _componentModelTypes;

        public static void GenerateTypesDictionary(Assembly modelsAssembly)
        {
            _componentModelTypes.Clear();

            foreach(var assemblyType in modelsAssembly.GetTypes())
            {
                var componentModelAttribute = assemblyType.GetCustomAttribute<ComponentModelAttribute>();

                if(componentModelAttribute is not null)
                    _componentModelTypes.Add(assemblyType, componentModelAttribute);
            }
        }
    }
}
