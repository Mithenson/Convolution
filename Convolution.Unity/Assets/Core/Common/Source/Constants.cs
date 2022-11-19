using UnityEngine;

namespace Convolution
{
	public static class Constants
	{
		public const string OwnedLayerName = "Convolution";

		public readonly static int OwnedLayer = LayerMask.NameToLayer(OwnedLayerName);
	}
}