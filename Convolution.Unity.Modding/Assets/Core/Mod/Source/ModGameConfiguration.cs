using Convolution.DevKit.MiniGames;
using UnityEngine;
using Zenject;

namespace Modder.Mod
{
	[CreateAssetMenu(menuName = "Modder/Mod/Game configuration", fileName = nameof(ModGameConfiguration))]
	public sealed class ModGameConfiguration : MiniGameConfiguration<ModGame>
	{
		[SerializeField]
		private GameObject _playerPrefab;

		[SerializeField]
		private float _playerSpeed;

		public GameObject PlayerPrefab => _playerPrefab;
		public float PlayerSpeed => _playerSpeed;
        
		public override void BindDependencies(DiContainer container){ }
	}
}