namespace Maxim.AssetManagement.Editor
{
	public static class PathUtilities
	{
		public static string GetDirectory(string path) => GetParentDirectory(path);
		public static string GetParentDirectory(string directory)
		{
			var removalIndex = directory.LastIndexOf('/');
			if (removalIndex == -1)
				return null;

			return directory.Remove(removalIndex, directory.Length - removalIndex);
		}
	}
}