using Maxim.Common.Extensions;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Sample
{
	public sealed class Player : MonoBehaviour
	{
		[SerializeField]
		private float _speed;

		[SerializeField]
		private int _maxHealth;

		[SerializeField]
		private float _radius;
        
		private SampleMiniGame _game;
		private PlayerModel _model;

		[Inject]
		private void Inject(SampleMiniGame game, PlayerModel model)
		{
			_game = game;
			_model = model;

			_model.Health = _maxHealth;
		}

		public float Radius => _radius;
		public int MaxHealth => _maxHealth;
        
		public void Move(Vector2 input)
		{
			var newPosition = transform.position + (Vector3)(input * (Time.deltaTime * _speed));
			//transform.position = _game.CameraDisplay.Wrap(newPosition, _game.Configuration.WrapPadding);
		}
        
		public void Rotate(Vector2 input) => transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(-input.x, input.y) * Mathf.Rad2Deg);

		public void Hurt() => _model.Health--;

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