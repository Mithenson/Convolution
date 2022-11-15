using UnityEngine;

namespace Convolution.Interaction
{
	public abstract class Handle : MonoBehaviour
	{
		public abstract bool IsHovered(Cursor cursor, out float separation);
	}
}