using UnityEngine;

namespace Maxim.Common.Extensions
{
	public static class RectExtensions
	{
		public static Rect Inflate(this Rect rect, float amount)
		{
			var inflation = Vector2.one * amount;
			
			rect.position -= inflation;
			rect.size += inflation * 2.0f;

			return rect;
		}
	}
}