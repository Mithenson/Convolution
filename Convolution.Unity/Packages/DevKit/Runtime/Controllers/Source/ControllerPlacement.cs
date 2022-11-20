using System;
using UnityEngine;

namespace Convolution.DevKit.Controllers
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

		#if UNITY_EDITOR

		public static string PrefabFieldName => nameof(_prefab);
		public static string PositionFieldName => nameof(_position);
		public static string InputChannelFieldName => nameof(_inputChannel);
			
		#endif

		public Controller Prefab => _prefab;
		public Vector2Int Position => _position;
		public ushort InputChannel => _inputChannel;
	}
}