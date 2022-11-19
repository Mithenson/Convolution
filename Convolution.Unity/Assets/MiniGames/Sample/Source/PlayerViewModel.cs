using Maxim.MVVM;
using UnityEngine;

namespace Convolution.MiniGames.Sample
{
	public sealed class PlayerViewModel : ViewModel<PlayerModel>
	{
		private int _health;
		public int Health
		{
			get => _health;
			set => ChangeProperty(ref _health, Mathf.Clamp(value, 0, _configuration.PlayerMaxHealth));
		}
        
		private readonly SampleMiniGameConfiguration _configuration;
        
		public PlayerViewModel(PlayerModel model, SampleMiniGameConfiguration configuration) : base(model)
		{
			_configuration = configuration;

			AddPropertyBinding(nameof(PlayerModel.Health), nameof(Health));
		}
	}
}