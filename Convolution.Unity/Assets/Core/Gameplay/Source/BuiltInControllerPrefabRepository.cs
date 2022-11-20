using System;
using System.Collections.Generic;
using Convolution.DevKit.Controllers;
using UnityEngine;

namespace Convolution.Interaction
{
	[CreateAssetMenu(menuName = "Convolution/Repository/Built in controller prefabs", fileName = nameof(BuiltInControllerPrefabRepository))]
	public sealed class BuiltInControllerPrefabRepository : ScriptableObject
	{
		#region Nested types

		[Serializable]
		private sealed class Entry
		{
			[SerializeField]
			private BuiltInControllerType _type;

			[SerializeField]
			private Controller _prefab;

			public Entry(BuiltInControllerType type, Controller prefab)
			{
				_type = type;
				_prefab = prefab;
			}

			public BuiltInControllerType Type => _type;
			public Controller Prefab => _prefab;
		}

		#endregion

		[SerializeField]
		private Entry[] _entries;

		private Dictionary<int, Controller> _prefabsByType;

		public Controller this[BuiltInControllerType type] => _prefabsByType[(int)type];

		public void Bootup()
		{
			_prefabsByType = new Dictionary<int, Controller>();
			foreach (var entry in _entries)
				_prefabsByType.Add((int)entry.Type, entry.Prefab);
		}

		public bool TryGet(BuiltInControllerType type, out Controller prefab) => _prefabsByType.TryGetValue((int)type, out prefab);
	}
}