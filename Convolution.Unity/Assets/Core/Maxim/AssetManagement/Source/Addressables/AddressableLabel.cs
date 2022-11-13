using UnityEngine;

namespace Maxim.AssetManagement.Addressables
{
	[CreateAssetMenu(menuName = "Haven/Asset Management/Addressable/Label", fileName = nameof(AddressableLabel))]
	public sealed class AddressableLabel : ScriptableObject
	{
		[SerializeField]
		private string _name;
	}
}