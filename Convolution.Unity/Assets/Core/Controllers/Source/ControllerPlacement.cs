using System;
using UnityEngine;

namespace Convolution.Controllers
{
	[Serializable]
	public sealed class ControllerPlacement
	{
		[SerializeField]
		private Controller _prefab;

		[SerializeField]
		private Vector2Int _position;
		
		[SerializeField]
		private ushort _inputChannel;

		public Controller Prefab => _prefab;
		public Vector2Int Position => _position;
		public ushort InputChannel => _inputChannel;
	}
}