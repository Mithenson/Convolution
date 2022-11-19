using Maxim.Common.Extensions;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Sample
{
	public sealed class Bullet : MonoBehaviour
	{
		[HideInInspector]
		public Vector2 Direction;
        
		[SerializeField]
		private float _speed;
        
		[SerializeField]
		private float _radius;
        
		private SampleMiniGame _game;

		[Inject]
		private void Inject(SampleMiniGame game) => _game = game;

		public void Tick(Player player, out bool isDestroyed)
		{
			var distance = ((Vector2)player.transform.position - (Vector2)transform.position).magnitude;
			if (distance < _radius + player.Radius)
			{
				player.Hurt();
                
				Destroy(gameObject);
				isDestroyed = true;

				return;
			}
			
			var displacement = Direction * (_speed * Time.deltaTime);
			var newPosition = (Vector2)transform.position + displacement;
			var inflatedBounds = _game.SpriteMaskDisplay.Bounds.Inflate(_game.Configuration.WrapPadding);
			
			if (!inflatedBounds.Contains(newPosition))
			{
				Destroy(gameObject);
				isDestroyed = true;

				return;
			}
			else
			{
				transform.position = newPosition;
			}

			isDestroyed = false;
		}

		#if UNITY_EDITOR

		private void OnDrawGizmosSelected()
		{
			var previousHandlesColor = UnityEditor.Handles.color;
			UnityEditor.Handles.color = Color.white.SetAlpha(0.25f);
            
			UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);

			UnityEditor.Handles.color = previousHandlesColor;
		}
        
		#endif
	}
}