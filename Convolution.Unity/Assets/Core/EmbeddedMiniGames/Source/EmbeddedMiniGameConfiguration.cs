using Convolution.DevKit.MiniGames;
using UnityEngine;

namespace Convolution.Core.EmbeddedMiniGames
{
	public interface IEmbeddedMiniGameConfiguration : IMiniGameConfiguration
	{
		MiniGameDefinition Definition { get; }
	}
	
	public abstract class EmbeddedMiniGameConfiguration<TGame> : MiniGameConfiguration<TGame>, IEmbeddedMiniGameConfiguration
		where TGame : MiniGame
	{
		[SerializeField]
		private MiniGameDefinition _definition;

		public MiniGameDefinition Definition => _definition;
	}
}