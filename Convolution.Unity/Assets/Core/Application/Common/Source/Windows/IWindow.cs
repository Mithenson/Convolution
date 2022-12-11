using System;
using UnityEngine;

namespace VirtCons.Internal.Application.Common.Source.Windows
{
	public interface IWindow
	{
		IntPtr Handle { get; }
        
		RectInt GetRect();
		void SetRect(RectInt rect);
		void MakeTransparent();
		void MakeBorderless();
		void Repaint();
	}
}