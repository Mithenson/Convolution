using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Maxim.Inputs
{
	[Serializable]
	public sealed class InputActionMapReference
	{
		[SerializeField]
		private InputActionAsset _asset;
		
		[SerializeField]
		private string _id;

		private Guid? _lazyId;
		
		#if UNITY_EDITOR

		public static string AssetFieldName => nameof(_asset);
		public static string IdFieldName => nameof(_id);
		
		#endif

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