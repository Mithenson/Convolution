using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	[CustomEditor(typeof(AddressableCollector))]
	public sealed class AddressableCollectorEditor : UnityEditor.Editor
	{
		private static bool _hasBeenInitializedGlobally;
		private static Type[] _namingStrategyTypes;
		private static GUIContent[] _namingStrategyContents;

		public override void OnInspectorGUI()
		{
			if (!_hasBeenInitializedGlobally)
				InitializeGlobally();

			var useDefaultGroupProperty = serializedObject.FindProperty(AddressableCollector.UseDefaultGroupFieldName);
			EditorGUILayout.PropertyField(useDefaultGroupProperty);

			if (!useDefaultGroupProperty.boolValue)
			{
				var groupProperty = serializedObject.FindProperty(AddressableCollector.GroupFieldName);
				EditorGUILayout.PropertyField(groupProperty);
			}
			
			var namingStrategyProperty = serializedObject.FindProperty(AddressableCollector.NamingStrategyFieldName);
			if (!TryGetManagedReferenceType(namingStrategyProperty, out var namingStrategyType))
			{
				var index = EditorGUILayout.Popup(new GUIContent(namingStrategyProperty.displayName), 0, _namingStrategyContents);

				if (index > 0)
					namingStrategyProperty.managedReferenceValue = Activator.CreateInstance(_namingStrategyTypes[index - 1]);
			}
			else
			{
				var index = Array.IndexOf(_namingStrategyTypes, namingStrategyType);
				if (index == -1)
				{
					namingStrategyProperty.managedReferenceValue = null;
				}
				else
				{
					var selectedIndex = EditorGUILayout.Popup(new GUIContent(namingStrategyProperty.displayName), index + 1, _namingStrategyContents) - 1;
					
					if (selectedIndex == -1)
						namingStrategyProperty.managedReferenceValue = null;
					else if (index != selectedIndex)
						namingStrategyProperty.managedReferenceValue = Activator.CreateInstance(_namingStrategyTypes[selectedIndex]);
				}
			}
			
			var labelsProperty = serializedObject.FindProperty(AddressableCollector.LabelsFieldName);
			EditorGUILayout.PropertyField(labelsProperty);
			
			var manuelEntriesProperty = serializedObject.FindProperty(AddressableCollector.ManualEntriesFieldName);
			EditorGUILayout.PropertyField(manuelEntriesProperty);

			serializedObject.ApplyModifiedProperties();
		}

		private void InitializeGlobally()
		{
			_namingStrategyTypes = AppDomain.CurrentDomain.GetAssemblies()
			   .SelectMany(assembly => assembly.GetTypes())
			   .Where(type => !type.IsAbstract && typeof(IAddressableNamingStrategy).IsAssignableFrom(type))
			   .ToArray();

			_namingStrategyContents = new GUIContent[] { new GUIContent("None") }
			   .Concat(_namingStrategyTypes.Select(type => new GUIContent(type.Name)))
			   .ToArray();
			
			_hasBeenInitializedGlobally = true;
		}
		
		private static bool TryGetManagedReferenceType(SerializedProperty property, out Type type)
		{
			type = null;
			
			var split = property.managedReferenceFullTypename.Split(' ');
			if (split.Length != 2)
				return false;
			
			type = Type.GetType($"{split[1].Replace('/', '+')}, {split[0]}");
			return type != null;
		}
	}
}