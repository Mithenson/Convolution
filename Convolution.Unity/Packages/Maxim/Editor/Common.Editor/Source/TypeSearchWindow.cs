using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Maxim.Common.Editor
{
	public class TypeSearchWindow : ValueSearchWindow<Type>
	{
		public Type SourceType { get; private set; }

		public bool TryGetFirstAvailableType(out Type type)
		{
			type = null;
			
			if (!SearchTrees.TryGetValue(Key, out var searchTreeEntries)
			    || searchTreeEntries.Count < 2)
				return false;

			type = (Type)searchTreeEntries[1].userData;
			return true;
		}
		
		public void Initialize(string message, IEnumerable<Assembly> assemblies, Type sourceType, string defaultCategoryName = InvalidCategory) =>
			Initialize(message, assemblies, sourceType.Name, sourceType, defaultCategoryName);
		public void Initialize(string message, IEnumerable<Assembly> assemblies, string key, Type sourceType, string defaultCategoryName = InvalidCategory)
		{
			var types = assemblies.SelectMany(assembly => assembly.GetTypes())
			   .Where(type => !type.IsAbstract && !type.IsGenericType && sourceType.IsAssignableFrom(type));

			SourceType = sourceType;
			Initialize(message, key, types, defaultCategoryName);
		}
		
		public override string GetNameFor(Type value)
		{
			var attribute = value.GetCustomAttribute<SearchNameAttribute>();
			if (attribute != null)
				return attribute.Value;

			return value.Name;
		}
		protected override bool TryGetOverrideNameFor(Type value, out string name)
		{
			name = null;
			return false;
		}

		protected override bool TryGetCategoryNameFor(Type value, out string categoryName)
		{
			var attribute = value.GetCustomAttribute<SearchPathAttribute>();

			if (attribute != null)
			{
				categoryName = attribute.Value;
				return true;
			}

			categoryName = string.Empty;
			return false;
		}
	}
}