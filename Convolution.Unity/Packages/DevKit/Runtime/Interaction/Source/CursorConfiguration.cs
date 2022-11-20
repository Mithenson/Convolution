﻿using Maxim.AssetManagement.Configurations;
using UnityEngine;

namespace Convolution.DevKit.Interaction
{
	[CreateAssetMenu(menuName = "Convolution/Configurations/Cursor", fileName = nameof(CursorConfiguration))]
	public sealed class CursorConfiguration : Configuration
	{
		[SerializeField]
		private float _radius;

		public float Radius => _radius;
	}
}