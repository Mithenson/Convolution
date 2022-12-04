using UnityEngine;

namespace Convolution.MiniGames.Karting
{
	public sealed class Kart : MonoBehaviour
	{
		[SerializeField]
		private Collider2D _collider;

		public Collider2D Collider => _collider;
	}
}