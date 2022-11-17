using System;
using System.Linq;
using Maxim.AssetManagement.Addressables;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	[CreateAssetMenu(menuName = "Maxim/Asset management/Addressable/Collector", fileName = nameof(AddressableCollector))]
	public class AddressableCollector : ScriptableObject
	{
		[SerializeField]
		private AddressableAssetGroup _group;
		
		[SerializeReference]
		private IAddressableNamingStrategy _namingStrategy;

		[SerializeField]
		private AddressableLabel[] _labels;

		private IAddressableNamingStrategy NamingStrategy => _namingStrategy ??= new NameAddressableByFileName();

		public void CreateOrAddAddressableEntry(GUID guid)
		{
			var entry = _group.Settings.CreateOrMoveEntry(guid.ToString(), _group);
			entry.SetAddress(NamingStrategy.GetName(guid));
			
			entry.labels.Clear();
			foreach (var label in _labels)
				entry.SetLabel(label.name, true, true);
		}
	}
}