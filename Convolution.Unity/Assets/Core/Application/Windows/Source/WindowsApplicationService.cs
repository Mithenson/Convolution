using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VirtCons.Internal.Application.Common.Source;

namespace VirtCons.Internal.Core.Application.Windows.Source
{
	public sealed class WindowsApplicationService : IApplicationService
	{
		#region Nested types

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;
		}

		#endregion
		
		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out POINT lpPoint);

		public Vector2Int GetMousePosition()
		{
			if (!GetCursorPos(out var cursor))
				throw new InvalidOperationException("Couldn't get the cursor's position.");

			return new Vector2Int(cursor.X, cursor.Y);
		}
	}
}