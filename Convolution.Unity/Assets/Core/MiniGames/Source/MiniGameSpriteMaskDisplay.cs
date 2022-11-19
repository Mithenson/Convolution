﻿using Maxim.Common.Extensions;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Source
{
	public sealed class MiniGameSpriteMaskDisplay : MonoBehaviour, IMiniGameDisplay
	{
		[SerializeField]
		private Canvas _canvas;
		
		[SerializeField]
		private RenderTexture _render;
		
		private SpriteMask _mask;
		private MiniGameRenderer _renderer;

		private void Awake() => _mask = GetComponentInChildren<SpriteMask>();

		[Inject]
		private void Inject(MiniGameRenderer renderer) => _renderer = renderer;

		public void Bootup()
		{
			_renderer.Set(_render);
			
			gameObject.SetActive(true);
		}

		public Canvas Canvas => _canvas;
		public RenderTexture Render => _render;
		public Rect Bounds => new Rect(_mask.bounds.min, _mask.bounds.size);

		public Vector2 Wrap(Vector2 position, float boundsPadding = 0.0f)
		{
			var paddedBounds = Bounds.Inflate(boundsPadding);
			
			if (position.x > paddedBounds.xMax)
				position.x = paddedBounds.xMin;
			else if (position.x < paddedBounds.xMin)
				position.x = paddedBounds.xMax;
                    
			if (position.y > paddedBounds.yMax)
				position.y = paddedBounds.yMin;
			else if (position.y < paddedBounds.yMin)
				position.y = paddedBounds.yMax;

			return position;
		}
	}
}