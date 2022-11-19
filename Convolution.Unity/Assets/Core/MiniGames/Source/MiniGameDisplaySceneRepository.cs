using System.Collections.Generic;
using UnityEngine;

namespace Convolution.MiniGames.Source
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