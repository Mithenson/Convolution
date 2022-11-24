using Convolution.Core.EmbeddedMiniGames;
using Convolution.DevKit.MiniGames;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Sample
{
	[CreateAssetMenu(menuName = "Convolution/MiniGames/Sample", fileName = nameof(SampleMiniGameConfiguration))]
	public sealed class SampleMiniGameConfiguration : EmbeddedMiniGameConfiguration<SampleMiniGame>
	{
		[SerializeField]
		private GameObject _uiPrefab;
		
		[SerializeField]
		private Player _playerPrefab;

		[SerializeField]
		private Bullet _bulletPrefab;
		
		[SerializeField]
		private float _bulletSpawnInterval;

		[SerializeField]
		private float _wrapPadding;

		[SerializeField]
		private float _neededSurvivalDuration;
		
		public GameObject UIPrefab => _uiPrefab;
		public Player PlayerPrefab => _playerPrefab;
		public int PlayerMaxHealth => _playerPrefab.MaxHealth;
		public Bullet BulletPrefab => _bulletPrefab;
		public float BulletSpawnInterval => _bulletSpawnInterval;
		public float WrapPadding => _wrapPadding;
		public float NeededSurvivalDuration => _neededSurvivalDuration;

		public override void BindDependencies(DiContainer container)
		{
			container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle();
			container.BindInterfacesAndSelfTo<PlayerViewModel>().AsSingle();
			
			container.BindInterfacesAndSelfTo<TimerModel>().AsSingle();
			container.BindInterfacesAndSelfTo<TimerViewModel>().AsSingle();
		}
	}
}