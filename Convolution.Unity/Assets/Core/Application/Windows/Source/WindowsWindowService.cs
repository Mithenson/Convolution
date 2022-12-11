using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VirtCons.Internal.Application.Common.Source.Windows;

namespace VirtCons.Internal.Core.Application.Windows.Source
{
	public sealed class WindowsWindowService : IWindowService
	{
		private WindowsWindow _mainWindow;
		
		private WindowsWindowService()
		{
			_mainWindow = new WindowsWindow(GetActiveWindow());
			_mainWindow.MakeTransparent();
			_mainWindow.MakeBorderless();
			
			var width = Screen.currentResolution.width / 2;
			var height = Screen.currentResolution.height / 2;
			var x = width / 2;
			var y = height / 2;
			_mainWindow.SetRect(new RectInt(x, y, width, height));
		}

		public IWindow MainWindow => _mainWindow;
		
		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();
	}
}