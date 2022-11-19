using UnityEngine;

namespace Maxim.Common.Extensions
{
	public static class GameObjectExtensions
	{
		public static void SetLayer(this GameObject gameObject, int layer, bool assignToChildren = true)
		{
			gameObject.layer = layer;

			if (!assignToChildren)
				return;

			foreach (Transform child in gameObject.transform)
				SetLayer(child.gameObject, layer, true);
		}
	}
}