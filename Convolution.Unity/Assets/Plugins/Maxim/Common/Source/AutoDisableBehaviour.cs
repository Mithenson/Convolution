using UnityEngine;

namespace Maxim.Common
{
	[DefaultExecutionOrder(-5000)]
	public sealed class AutoDisableBehaviour : MonoBehaviour
	{
		private void OnEnable()
		{
			gameObject.SetActive(false);
			Destroy(this);
		}
	}
}