using UnityEngine;

namespace Convolution
{
	public abstract class HelperBehaviour : MonoBehaviour
	{
		public Vector2 Position => new Vector2(transform.position.x, transform.position.z);
	}
}