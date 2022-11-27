using Convolution.DevKit.Interaction;
using UnityEngine;
using Cursor = Convolution.DevKit.Interaction.Cursor;

namespace Convolution.DevKit.Controllers
{
	public class WheelController : Controller
	{
		[SerializeField]
		private Transform _center;
		
		[SerializeField]
		private TorusHandle _handle;

		[SerializeField]
		private bool _useGainCurve;

		[SerializeField]
		private AnimationCurve _gainCurve;

		[SerializeField]
		private float _resetSpeed;

		[SerializeField]
		private float _spinRestFactor;

		private float _input;
		private float _usedResetSpeed;

		public override Handle Handle => _handle;

		public override IControllerInput ComputeInput() => new SimpleControllerInput<float>(_input);

		protected override bool IMP_TryStartInteraction(Cursor cursor) => true;

		protected override bool IMP_TryPerpetuateInteraction(Cursor cursor)
		{
			var center = (Vector2)_center.position;
			var from = cursor.LastPosition - center;
			var to = cursor.Position - center;
			
			var diff = Vector2.SignedAngle(from, to);

			if (_useGainCurve)
				diff *= _gainCurve.Evaluate(Mathf.Abs(_input));
			
			_input += diff;
			
			_handle.transform.parent.Rotate(Vector3.forward, diff);
			return true;
		}

		protected override void EXT_EndInteraction(Cursor cursor) => _usedResetSpeed = _resetSpeed + Mathf.Abs(_input) * _spinRestFactor;

		public override bool IMP_TryPerpetuateRecuperation()
		{
			if (Mathf.Approximately(_input, 0.0f))
			{
				_handle.transform.parent.rotation = Quaternion.identity;
				_input = 0.0f;
				
				return false;
			}

			var sign = (int)Mathf.Sign(_input);
			var diff = _usedResetSpeed * (Time.deltaTime * -sign);
			
			_input += diff;
			if ((int)Mathf.Sign(_input) != sign)
			{
				_handle.transform.parent.rotation = Quaternion.identity;
				_input = 0.0f;
				
				return false;
			}
			else
			{
				_handle.transform.parent.Rotate(Vector3.forward, diff);
				return true;
			}
		}
	}
}