using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Maxim.Common.Editor
{
	public abstract class ValueSearchWindow<T> : ScriptableObject, ISearchWindowProvider
	{
		protected const string InvalidCategory = "Invalid";
	    
		#region Static

		private static Texture2D Placeholder
		{
			get
			{
				if (!_hasPlaceholder)
				{
					_placeholder = new Texture2D(1,1);
					_placeholder.SetPixel(0,0, new Color(0,0,0,0));
					_placeholder.Apply();
				}

				return _placeholder;
			}   
		}
		
		protected static Dictionary<string, List<SearchTreeEntry>> SearchTrees = new Dictionary<string, List<SearchTreeEntry>>();

		private static Texture2D _placeholder;
		private static bool _hasPlaceholder = false;

		#endregion

		public event Action<object, Vector2> onSelectEntry;

		protected string Key => _key;
		protected string Message => _message;

		#region Fields

		private string _key;
		private string _message;

		#endregion
		
		public void Initialize(string message, string key, IEnumerable<T> values, string defaultCategoryName = InvalidCategory)
		{
			_key = key;
			_message = message;

			if (SearchTrees.ContainsKey(key))
				return;

			var searchCatalogue = new DropdownSearchCategory<T>("Root");
			foreach (var value in values)
			{
				var categoryName = defaultCategoryName;
				
				if (TryGetCategoryNameFor(value, out var foundCategoryName))
				{
					if (!searchCatalogue.TryGet(foundCategoryName, out var category))
						continue;

					category.Add(value);
				}
				else if (categoryName != InvalidCategory)
				{
					if (!searchCatalogue.TryGet(defaultCategoryName, out var category))
						continue;

					category.Add(value);
				}
				else searchCatalogue.Add(value);
			}
			
			var searchTree = new List<SearchTreeEntry>() { new SearchTreeGroupEntry(new GUIContent(_message), 0) };
			foreach (var value in searchCatalogue.Values)
			{
				var name = GetNameFor(value);
            					
				if (TryGetOverrideNameFor(value, out var overrideName))
					name = overrideName;
				
				searchTree.Add(new SearchTreeEntry(new GUIContent(name, Placeholder))
				{
					userData = value,
					level = 1
				});
			}
            
			searchCatalogue.Relay((category, depth) =>
			{
				searchTree.Add(new SearchTreeGroupEntry(new GUIContent(category.Name), depth));
                
				foreach (var value in category.Values)
				{
					var name = GetNameFor(value);
					
					if (TryGetOverrideNameFor(value, out var overrideName))
						name = overrideName;
                    
					searchTree.Add(new SearchTreeEntry(new GUIContent(name, Placeholder))
					{
						userData = value,
						level = depth + 1
					});
				}
			}, 0, false);
			
			SearchTrees.Add(key, searchTree);
		}

		public void ResetKey(string key) => 
			_key = key;

		#region Members

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) =>
			SearchTrees[_key];
		
		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
		{
			var mousePosition = context.screenMousePosition;
			onSelectEntry?.Invoke(searchTreeEntry.userData, mousePosition);
            
			return true;
		}

		#endregion
		
		public abstract string GetNameFor(T value);
		protected abstract bool TryGetOverrideNameFor(T value, out string name);
		protected abstract bool TryGetCategoryNameFor(T value, out string categoryName);
	}
}