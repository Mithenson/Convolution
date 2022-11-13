using Maxim.AssetManagement.Addressables;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	[CreateAssetMenu(menuName = "Haven/Asset Management/Addressable/Collector", fileName = nameof(AddressableCollector))]
	public class AddressableCollector : ScriptableObject
	{
		[SerializeField]
		private AddressableAssetGroup _group;
		
		[SerializeReference]
		private IAddressableNamingStrategy _namingStrategy;

		[SerializeField]
		private AddressableLabel[] _labels;

		public void CreateOrAddAddressableEntry(GUID guid)
		{
			var entry = _group.Settings.CreateOrMoveEntry(guid.ToString(), _group);
			entry.SetAddress(_namingStrategy.GetName(guid));
			
			entry.labels.Clear();
			foreach (var label in _labels)
				entry.SetLabel(label.name, true, true);
		}
	}
}