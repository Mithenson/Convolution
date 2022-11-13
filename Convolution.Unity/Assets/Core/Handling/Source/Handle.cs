using System;
using UnityEngine;

namespace Convolution.Handling
{
	public abstract class Handle : HelperBehaviour
	{
		public abstract bool IsHovered(Cursor cursor, out float separation);
	}
}