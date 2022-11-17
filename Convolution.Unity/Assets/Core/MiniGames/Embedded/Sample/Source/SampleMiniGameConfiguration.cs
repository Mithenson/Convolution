using Convolution.MiniGames.Source;
using UnityEngine;

namespace Sample
{
	[CreateAssetMenu(menuName = "Convolution/MiniGames/Sample", fileName = nameof(SampleMiniGameConfiguration))]
	public sealed class SampleMiniGameConfiguration : MiniGameConfiguration<SampleMiniGame.InputChannel, SampleMiniGame>
	{
		[SerializeField]
		private GameObject _playerPrefab;

		[SerializeField]
		private float _playerSpeed;

		public GameObject PlayerPrefab => _playerPrefab;
		public float PlayerSpeed => _playerSpeed;
	}
}