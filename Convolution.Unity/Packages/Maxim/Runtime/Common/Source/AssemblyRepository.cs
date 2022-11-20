using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Maxim.Common.Tags;
using Maxim.Common.Utilities;
using UnityEngine;

namespace Maxim.Common
{
    [CreateAssetMenu(menuName = "Maxim/Assembly Repository", fileName = nameof(AssemblyRepository))]
    public class AssemblyRepository : ScriptableObject, ITagged
    {
        [SerializeReference]
        private Tag _tag;
        
        [SerializeField]
        private AssemblyReference[] _assemblyReferences;

        private Assembly[] _lazyAssemblies;

        public Tag Tag => _tag;

        public Assembly[] GetAssemblies()
        {
            if (_lazyAssemblies != null)
                return _lazyAssemblies;

            _lazyAssemblies = _assemblyReferences
               .Select(assemblyReference => assemblyReference.Value)
               .ToArray();

            return _lazyAssemblies;
        }

        public static IEnumerable<Assembly> GetAllAssembliesFromTag(Tag tag)
        {
            var assemblyRepositories = ResourceUtilities.LoadAllByTag<AssemblyRepository>(tag);
            return assemblyRepositories.SelectMany(repository => repository.GetAssemblies()).Distinct();
        }
    }
}
