using UnityEngine;

namespace Maxim.AssetManagement.Addressables
{
	[CreateAssetMenu(menuName = "Maxim/Asset management/Addressable/Label", fileName = nameof(AddressableLabel))]
	public sealed class AddressableLabel : ScriptableObject
	{
		[SerializeField]
		private string _name;

		public string Name => _name;
	}
}