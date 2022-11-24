using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Convolution.DevKit.MiniGames
{
	[Serializable]
	public sealed class MiniGameDefinition
	{
		[SerializeField]
		[JsonProperty(nameof(Name))]
		private string _name;

		[JsonIgnore]
		public string Name => _name;
	}
}