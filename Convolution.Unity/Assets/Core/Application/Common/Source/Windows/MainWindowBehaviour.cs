using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace VirtCons.Internal.Application.Common.Source.Windows
{
	public sealed class MainWindowBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler
	{
		private IApplicationService _applicationService;
		private IWindowService _windowService;

		private Vector2Int _mousePositionAtDragStart;
		private Vector2Int _windowPositionAtDragStart;

		[Inject]
		private void Inject(IApplicationService applicationService, IWindowService windowService)
		{
			_applicationService = applicationService;
			_windowService = windowService;
		}
		public void OnBeginDrag(PointerEventData eventData)
		{
			_mousePositionAtDragStart = _applicationService.GetMousePosition();
			
			var rect = _windowService.MainWindow.GetRect();
			_windowPositionAtDragStart = rect.position;
		}
		
		public void OnDrag(PointerEventData eventData)
		{
			var delta = _applicationService.GetMousePosition() - _mousePositionAtDragStart;
			
			var rect = _windowService.MainWindow.GetRect();
			_windowService.MainWindow.SetRect(new RectInt(_windowPositionAtDragStart + delta, rect.size));
		}
	}
}