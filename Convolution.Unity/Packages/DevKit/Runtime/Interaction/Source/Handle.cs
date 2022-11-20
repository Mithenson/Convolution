﻿using UnityEngine;

namespace Convolution.DevKit.Interaction
{
	public abstract class Handle : MonoBehaviour
	{
		public abstract bool IsHovered(Cursor cursor, out float separation);
	}
}