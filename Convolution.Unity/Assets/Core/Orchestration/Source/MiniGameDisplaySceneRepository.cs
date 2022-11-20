using System.Collections.Generic;
using Convolution.DevKit.MiniGames;
using UnityEngine;

namespace Convolution.Orchestration
{
	public sealed class MiniGameDisplaySceneRepository : MonoBehaviour
	{
		private IMiniGameDisplay[] _displays;
		
		private void Awake()
		{
			_displays ??= GetComponentsInChildren<IMiniGameDisplay>(true);
		}

		public IReadOnlyList<IMiniGameDisplay> Displays
		{
			get
			{
				_displays ??= GetComponentsInChildren<IMiniGameDisplay>(true);
				return _displays;
			}
		}
	}
}