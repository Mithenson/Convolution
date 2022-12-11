using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VirtCons.Internal.Application.Common.Source.Windows;

namespace VirtCons.Internal.Core.Application.Windows.Source
{
	public sealed class WindowsWindow : IWindow
	{
		#region Nested types

		[StructLayout(LayoutKind.Sequential)]
		private struct MARGINS
		{
			public int CxLeftWidth;
			public int CxRightWidth;
			public int CyTopHeight;
			public int CyBottomHeight;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		#endregion

		#region Constants

		private const int GWL_STYLE = -16;
		private const uint WS_BORDER = 0x00800000;
		private const uint WS_CAPTION = 0x00C00000;
		private const uint WS_SYSMENU = 0x00080000;
		private const uint WS_THICKFRAME = 0x00040000;
		private const uint WS_MINIMIZE = 0x20000000;
		private const uint WS_MAXIMIZEBOX = 0x00010000;
        
		private const int GWL_EXSTYLE = -20;
		private const uint WS_EX_LAYERED = 0x00080000;
		private const uint WS_EX_TRANSPARENT = 0x00000020;

		private const uint SWP_NOSIZE = 0x0001;
		private const uint SWP_NOMOVE = 0x0002;
		private const uint SWP_FRAMECHANGED = 0x0020;

		#endregion
		
		private static readonly IntPtr HWN_TOPMOST = new IntPtr(-1);
		
		private readonly IntPtr _handle;

		public IntPtr Handle => _handle;

		public WindowsWindow(IntPtr handle) => _handle = handle;

		[DllImport("user32.dll")]
		public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        
		[DllImport("Dwmapi.dll")]
		private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

		public RectInt GetRect()
		{
			if (!GetWindowRect(_handle, out var rect))
				throw new InvalidOperationException("Could not get the active window's rect.");

			return new RectInt(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
		}
		public void SetRect(RectInt rect) => SetWindowPos(_handle, IntPtr.Zero, rect.x, rect.y, rect.width, rect.height, SWP_FRAMECHANGED);
		
		public void MakeTransparent()
		{
			var margins = new MARGINS() { CxLeftWidth = -1 };
			DwmExtendFrameIntoClientArea(_handle, ref margins);
			SetWindowLong(_handle, GWL_EXSTYLE, WS_EX_LAYERED);
		}
		public void MakeBorderless()
		{
			var style = GetWindowLong(_handle, GWL_STYLE);
			style &= ~(WS_BORDER | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME);
			SetWindowLong(_handle, GWL_STYLE, style);
		}
		
		public void Repaint() => SetWindowPos(_handle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE| SWP_NOSIZE | SWP_FRAMECHANGED);
	}
}