using System;
using UnityEngine;

namespace Convolution.MiniGames.Source
{
	public abstract class MiniGameConfiguration : ScriptableObject
	{
		public abstract Type GameType { get; }
	}
	
	public abstract class MiniGameConfiguration<TGame> : MiniGameConfiguration
		where TGame : MiniGame
	{
		public override Type GameType => typeof(TGame);
	}
}