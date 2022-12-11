using System;
using UnityEngine;

namespace VirtCons.Internal.Application.Common.Source.Windows
{
	public sealed class NullWindow : IWindow
	{
		public IntPtr Handle => IntPtr.Zero;

		public RectInt GetRect() => default;
		public void SetRect(RectInt rect) { }
		public void MakeTransparent() { }
		public void MakeBorderless() { }
		public void Repaint() { }
	}
}