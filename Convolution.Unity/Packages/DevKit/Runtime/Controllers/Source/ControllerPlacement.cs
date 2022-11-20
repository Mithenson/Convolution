using System;
using UnityEngine;

namespace Convolution.DevKit.Controllers
{
	[Serializable]
	public sealed class ControllerPlacement
	{
		#region Nested types

		public enum SelectionMode
		{
			BuiltIn,
			Custom
		}

		#endregion

		[SerializeField]
		private SelectionMode _chosenSelectionMode;
		
		[SerializeField]
		private BuiltInControllerType _pickedBuiltInType;
		
		[SerializeField]
		private Controller _pickedPrefab;

		[SerializeField]
		private Vector2Int _position;
		
		[SerializeField]
		private ushort _inputChannel;

		#if UNITY_EDITOR

		public static string ChosenSelectionModeFieldName => nameof(_chosenSelectionMode);
		public static string PickedBuiltInTypeFieldName => nameof(_pickedBuiltInType);
		public static string PickedPrefabFieldName => nameof(_pickedPrefab);
		public static string PositionFieldName => nameof(_position);
		public static string InputChannelFieldName => nameof(_inputChannel);
			
		#endif

		public SelectionMode ChosenSelectionMode => _chosenSelectionMode;
		public BuiltInControllerType PickedBuiltInType => _pickedBuiltInType;
		public Controller PickedPrefab => _pickedPrefab;
		public Vector2Int Position => _position;
		public ushort InputChannel => _inputChannel;
	}
}