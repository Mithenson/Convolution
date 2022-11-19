using System;
using System.Collections.Generic;
using Convolution.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Source
{
	public abstract class MiniGameConfiguration : ScriptableObject
	{
		public abstract Type GameType { get; }
		public abstract IReadOnlyList<ControllerPlacement> ControllerPlacements { get; }

		public abstract void BindDependencies(DiContainer container);
	}
	
	public abstract class MiniGameConfiguration<TGame> : MiniGameConfiguration
		where TGame : MiniGame
	{
		[SerializeField]
		private ControllerPlacement[] _controllerPlacements;
		
		public override Type GameType => typeof(TGame);
		public override IReadOnlyList<ControllerPlacement> ControllerPlacements => _controllerPlacements;
	}
}