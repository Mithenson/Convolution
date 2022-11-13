using System.Collections.Generic;
using System.Linq;
using Maxim.Common.Tags;
using UnityEngine;

namespace Maxim.Common.Utilities
{
	public static class ResourceUtilities
	{
		public static IEnumerable<TAsset> LoadAllByTag<TAsset>(Tag tag)
			where TAsset : Object, ITagged
		{
			var assets = Resources.LoadAll<TAsset>(null);
			return assets.Where(asset => asset.Tag == tag);
		}
	}
}