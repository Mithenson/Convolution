using Convolution.DevKit.Interaction;
using UnityEngine;
using Cursor = Convolution.DevKit.Interaction.Cursor;

namespace Convolution.DevKit.Controllers
{
	public class LeverController : Controller
	{
		[SerializeField]
		private Transform _center;

		[SerializeField]
		private CompositeHandle _handle;
		
		[SerializeField]
		[Min(0.1f)]
		private float _extent;

		[SerializeField]
		private Vector2 _mapping;

		[SerializeField]
		[Range(-1.0f, 1.0f)]
		private float _restingValue;
		
		[SerializeField]
		private bool _resetInputOnInteractionEnd;

		private float _input;
		
		public override Handle Handle => _handle;

		public override IControllerInput ComputeInput() => new SimpleControllerInput<float>(_input);

		protected override bool IMP_TryStartInteraction(Cursor cursor) => true;

		protected override bool IMP_TryPerpetuateInteraction(Cursor cursor)
		{
			var selfPosition = (Vector2)_center.position;
			var delta = (cursor.Position - selfPosition).y;

			var ratio = default(float);
			if (Mathf.Abs(delta) > _extent)
			{
				var sign = Mathf.Sign(delta);

				ratio = 1.0f * sign;
				delta = _extent * sign;
			}
			else
			{
				ratio = delta == 0.0f ? 0.0f : delta / _extent;
			}

			_input = Mathf.Lerp(_mapping.x, _mapping.y, Mathf.InverseLerp(-1.0f, 1.0f, ratio));
			_handle.transform.position = new Vector3(_handle.transform.position.x, selfPosition.y + delta, _handle.transform.position.z);
			
			return true;
		}

		protected override void EXT_EndInteraction(Cursor cursor)
		{
			if (!_resetInputOnInteractionEnd)
				return;
			
			var selfPosition = (Vector2)_center.position;
			_handle.transform.position = new Vector3( _handle.transform.position.x, selfPosition.y + _extent * _restingValue, _handle.transform.position.z);

			_input = Mathf.Lerp(_mapping.x, _mapping.y, Mathf.InverseLerp(-1.0f, 1.0f, _restingValue));
		}

		public override bool IMP_TryPerpetuateRecuperation() => false;
	}
}