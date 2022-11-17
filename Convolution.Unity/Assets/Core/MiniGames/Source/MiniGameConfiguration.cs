using System;
using System.Collections.Generic;
using Convolution.Controllers;
using UnityEngine;

namespace Convolution.MiniGames.Source
{
	public abstract class MiniGameConfiguration : ScriptableObject
	{
		public abstract Type GameType { get; }
		public abstract IReadOnlyList<ControllerPlacement> ControllerPlacements { get; }
	}
	
	public abstract class MiniGameConfiguration<TInputChannel, TGame> : MiniGameConfiguration
		where TInputChannel : Enum
		where TGame : MiniGame
	{
		[SerializeField]
		private ControllerPlacement[] _controllerPlacements;
		
		public override Type GameType => typeof(TGame);
		public override IReadOnlyList<ControllerPlacement> ControllerPlacements => _controllerPlacements;
	}
}