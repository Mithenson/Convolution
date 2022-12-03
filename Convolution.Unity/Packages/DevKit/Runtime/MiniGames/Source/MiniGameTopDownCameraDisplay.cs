using UnityEngine;
using Zenject;

namespace Convolution.DevKit.MiniGames
{
	public sealed class MiniGameTopDownCameraDisplay : MonoBehaviour, IMiniGameDisplay
	{
		[SerializeField]
		private Canvas _canvas;
		
		[SerializeField]
		private RenderTexture _render;

		[SerializeField]
		private Camera _camera;
		
		private MiniGameRenderer _renderer;

		[Inject]
		private void Inject(MiniGameRenderer renderer) => _renderer = renderer;

		public void Bootup()
		{
			_camera.targetTexture = _render;
			_renderer.Set(_render);
		}

		public Camera Camera => _camera;
		public Canvas Canvas => _canvas;
		public Rect Bounds
		{
			get
			{
				var height = _camera.orthographicSize * 2.0f;
				var size = new Vector2(_camera.orthographicSize * 2.0f, height * _camera.aspect);

				return new Rect((Vector2)transform.position - size * 0.5f, size);
			}
		}
		
		public void Show() => gameObject.SetActive(true);
		public void Hide() => gameObject.SetActive(false);
	}
}