using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Maxim.Inputs
{
	[Serializable]
	public sealed class InputActionMapReference
	{
		[SerializeField]
		private InputActionAsset _asset;

		#if UNITY_EDITOR
		[ShowIf(nameof(HasAsset))]
		[CustomValueDrawer(nameof(DrawGuid))]
		#endif
		[SerializeField]
		private string _id;

		private Guid? _lazyId;

		public Guid Id
		{
			get
			{
				_lazyId ??= Guid.Parse(_id);
				return _lazyId.Value;
			}
		}
        
		#if UNITY_EDITOR

		private bool HasAsset => _asset != null;

		private string DrawGuid(string value, GUIContent label)
		{
			if (_asset.actionMaps.Count == 0)
			{
				UnityEditor.EditorGUILayout.HelpBox("No maps are defined.", UnityEditor.MessageType.Warning);
				return string.Empty;
			}
            
			var id = !string.IsNullOrEmpty(value) ? Guid.Parse(value) : Guid.Empty;
			
			var selectedIndex = _asset.actionMaps.IndexOf(map => map.id == id);
			if (selectedIndex == -1)
				selectedIndex = 0;

			selectedIndex = UnityEditor.EditorGUILayout.Popup(label, selectedIndex, _asset.actionMaps.Select(map => new GUIContent(map.name)).ToArray());
			return _asset.actionMaps[selectedIndex].id.ToString();
		}

		#endif
	}
}