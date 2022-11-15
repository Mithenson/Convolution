using UnityEngine;

namespace Convolution.MiniGames.Source
{
	public sealed class SpriteMaskMiniGameDisplay : MonoBehaviour, IMiniGameDisplay
	{
		public void Show() => gameObject.SetActive(true);
		public void Hide() => gameObject.SetActive(false);
	}
}