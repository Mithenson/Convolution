using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Maxim.Common.Editor
{
	[CustomPropertyDrawer(typeof(AssemblyReference))]
	public sealed class AssemblyReferenceDrawer : PropertyDrawer
	{
		private static bool _hasBeenInitializedGlobally;
		private static SerializedProperty _activeProperty;
		private static AssemblySearchWindow _searchWindow;

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			if (!_hasBeenInitializedGlobally)
				InitializeGlobally();
			
			EditorGUI.BeginProperty(rect, label, property);
			property.NextVisible(true);

			var assembly = default(Assembly);
			try
			{
				assembly = Type.GetType(property.stringValue).Assembly;
			}
			catch
			{
				assembly = null;
			}
			
			var content = new GUIContent(assembly != null ? _searchWindow.GetNameFor(assembly) : "None");
			if (GUI.Button(rect, content))
			{
				_activeProperty = property.Copy();

				var position = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
				SearchWindow.Open(new SearchWindowContext(position), _searchWindow);
			}
			
			EditorGUI.EndProperty();
		}

		private void InitializeGlobally()
		{
			_searchWindow = ScriptableObject.CreateInstance<AssemblySearchWindow>();
			_searchWindow.Initialize("Search", nameof(Assembly), AppDomain.CurrentDomain.GetAssemblies());
			_searchWindow.onSelectEntry += (data, _) =>
			{
				var assembly = (Assembly)data;
				_activeProperty.stringValue = assembly.GetTypes()[0].AssemblyQualifiedName;

				_activeProperty.serializedObject.ApplyModifiedProperties();
			};
			
			_hasBeenInitializedGlobally = true;
		}
	}
}
