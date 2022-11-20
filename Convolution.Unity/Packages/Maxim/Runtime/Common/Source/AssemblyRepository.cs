using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Maxim.Common.Tags;
using Maxim.Common.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Maxim.Common
{
    [CreateAssetMenu(menuName = "Maxim/Assembly Repository", fileName = nameof(AssemblyRepository))]
    public class AssemblyRepository : ScriptableObject, ITagged
    {
        [SerializeReference]
        private Tag _tag;
        
        #if UNITY_EDITOR
        [ValueDropdown(nameof(GetAllAssemblyNames))]
        #endif
        [SerializeField]
        [LabelText("Assemblies")]
        private string[] _assemblyNames;

        private Assembly[] _assemblies;

        public Tag Tag => _tag;

        public Assembly[] GetAssemblies()
        {
            if (_assemblies != null)
                return _assemblies;

            _assemblies = _assemblyNames
               .Select(typeName => Assembly.GetAssembly(Type.GetType(typeName)))
               .ToArray();

            return _assemblies;
        }

        public static IEnumerable<Assembly> GetAllAssembliesFromTag(Tag tag)
        {
            var assemblyRepositories = ResourceUtilities.LoadAllByTag<AssemblyRepository>(tag);
            return assemblyRepositories.SelectMany(repository => repository.GetAssemblies()).Distinct();
        }
    
        #if UNITY_EDITOR

        private IList<ValueDropdownItem<string>> GetAllAssemblyNames()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var outputs = new List<ValueDropdownItem<string>>();
            
            foreach (var assembly in assemblies)
            {
                var info = assembly.GetName();
                var types = assembly.GetTypes();

                if (!types.Any())
                    continue;
                
                var value = types[0].AssemblyQualifiedName;
                outputs.Add(new ValueDropdownItem<string>(info.Name, value));
            }

            return outputs.ToArray();
        }
    
        #endif
    }
}
