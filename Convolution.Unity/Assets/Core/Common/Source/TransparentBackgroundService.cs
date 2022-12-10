using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem.UI;
using Zenject;

#if true

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

#endif

namespace VirtCons.Internal.Common.Source
{
    public sealed class TransparentBackgroundService : IInitializable, ITickable
    {
        #if true
        
        #region Nested types

        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        #endregion

        private const int GWL_EXSTYLE = -20;
        private const uint WS_EX_LAYERED = 0x00080000;
        private const uint WS_EX_TRANSPARENT = 0x00000020;

        private static readonly IntPtr HWN_TOPMOST = new IntPtr(-1);
        
        private readonly EventSystem _eventSystem;
        
        private IntPtr _hWnd;

        private TransparentBackgroundService(EventSystem eventSystem) => _eventSystem = eventSystem;
            
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        
        [DllImport("Dwmapi.dll")]
        private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
        
        void IInitializable.Initialize()
        {
            _hWnd = GetActiveWindow();
            
            var margins = new MARGINS() { cxLeftWidth = -1 };
            DwmExtendFrameIntoClientArea(_hWnd, ref margins);
            
            SetWindowLong(_hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
            SetWindowPos(_hWnd, HWN_TOPMOST, 0, 0, 0, 0, 0);
        }
        
        void ITickable.Tick()
        {
            var module = (InputSystemUIInputModule)_eventSystem.currentInputModule;
            module.point.asset.Enable();
            module.point.action.actionMap.Enable();
            module.point.action.Enable();
            var mousePosition = module.point.action.ReadValue<Vector2>();
            
            var eventData = new PointerEventData(_eventSystem);
            eventData.position = mousePosition;

            var results = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, results);

            var isHovering = results.Any(result => result.gameObject.layer == LayerMask.NameToLayer("UI"));
            var hWndNewLong = isHovering ? WS_EX_LAYERED : WS_EX_LAYERED | WS_EX_TRANSPARENT;
            
            SetWindowLong(_hWnd, GWL_EXSTYLE, hWndNewLong);
            
            Debug.LogError($"[{Time.time}][{mousePosition}] Hovering: {isHovering}");
        }
        
        #else
        
        void IInitializable.Initialize() { }
        void ITickable.Tick() { }
        
        #endif
    }
}
