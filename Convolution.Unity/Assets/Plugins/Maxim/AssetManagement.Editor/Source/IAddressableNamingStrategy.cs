using UnityEditor;

namespace Maxim.AssetManagement.Editor
{
	public interface IAddressableNamingStrategy
	{
		string GetName(GUID guid);
	}
}