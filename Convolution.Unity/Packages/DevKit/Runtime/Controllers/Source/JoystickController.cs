using Convolution.DevKit.Interaction;
using UnityEngine;

using Cursor = Convolution.DevKit.Interaction.Cursor;

namespace Convolution.DevKit.Controllers
{
	public class JoystickController : Controller
	{
		[SerializeField]
		private Transform _center;
		
		[SerializeField]
		private CircleHandle _handle;

		[SerializeField]
		[Min(0.1f)]
		private float _extent;

		[SerializeField]
		private bool _resetInputOnInteractionEnd;

		private Vector2 _input;

		public override Handle Handle => _handle;
		
		protected override void EXT_Awake()
		{
			if (_handle == null)
				_handle = GetComponentInChildren<CircleHandle>();
		}

		public override IControllerInput ComputeInput() => new SimpleControllerInput<Vector2>(_input);

		protected override bool IMP_TryStartInteraction(Cursor cursor) => true;

		protected override bool IMP_TryPerpetuateInteraction(Cursor cursor)
		{
			var selfPosition = (Vector2)_center.position;

			var delta = cursor.Position - selfPosition;
			var distance = delta.magnitude;

			if (distance > _extent)
			{
				var direction = delta.normalized;
				delta = direction * _extent;

				_input = direction;
			}
			else
			{
				_input = distance == 0.0f ? Vector2.zero : delta.normalized * (distance / _extent);
			}

			var stickPosition = selfPosition + delta;
			_handle.transform.position = new Vector3(stickPosition.x, stickPosition.y, _handle.transform.position.z);

			return true;
		}

		protected override void EXT_EndInteraction(Cursor cursor)
		{
			var selfPosition = (Vector2)_center.position;
			
			_handle.transform.position = new Vector3(selfPosition.x, selfPosition.y, _handle.transform.position.z);
			
			if (_resetInputOnInteractionEnd)
				_input = Vector2.zero;
		}

		public override bool IMP_TryPerpetuateRecuperation() => false;
	}
}