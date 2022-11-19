using UnityEngine;

namespace Convolution.MiniGames.Sample
{
	public sealed class HealthView : MonoBehaviour
	{
		[SerializeField]
		private GameObject[] _elements;

		public void Refresh(int amount)
		{
			for (var i = 0; i < amount; i++)
				_elements[i].SetActive(true);

			for (var i = amount; i < _elements.Length; i++)
				_elements[i].SetActive(false);
		}
	}
}