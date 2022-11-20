using Maxim.AssetManagement.Addressables;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	[CreateAssetMenu(menuName = "Maxim/Asset management/Addressable/Collector", fileName = nameof(AddressableCollector))]
	public sealed class AddressableCollector : ScriptableObject
	{
		[SerializeField]
		private bool _useDefaultGroup;
		
		[SerializeField]
		private AddressableAssetGroup _group;
		
		[SerializeReference]
		private IAddressableNamingStrategy _namingStrategy;

		[SerializeField]
		private AddressableLabel[] _labels;

		private IAddressableNamingStrategy LazyNamingStrategy => _namingStrategy ??= new NameAddressableByFileName();
		
		#if UNITY_EDITOR

		public static string UseDefaultGroupFieldName => nameof(_useDefaultGroup);
		public static string GroupFieldName => nameof(_group);
		public static string NamingStrategyFieldName => nameof(_namingStrategy);
		public static string LabelsFieldName => nameof(_labels);
			
		#endif

		public void CreateOrAddAddressableEntry(GUID guid)
		{
			var group = _useDefaultGroup ? AddressableAssetSettingsDefaultObject.Settings.DefaultGroup : _group;
			
			var entry = AddressableAssetSettingsDefaultObject.Settings.CreateOrMoveEntry(guid.ToString(), group);
			entry.SetAddress(LazyNamingStrategy.GetName(guid));
			
			entry.labels.Clear();
			foreach (var label in _labels)
				entry.SetLabel(label.name, true, true);
		}
	}
}