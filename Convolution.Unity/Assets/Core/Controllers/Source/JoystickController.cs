using UnityEngine;

using Cursor = Convolution.Interaction.Cursor;

namespace Convolution.Controllers
{
	public class JoystickController : Controller
	{
		[SerializeField]
		private Transform _stick;

		[SerializeField]
		[Min(0.1f)]
		private float _extent;

		[SerializeField]
		private bool _resetInputOnInteractionEnd;

		private Vector2 _input;

		public override IControllerInput ComputeInput() => new SimpleControllerInput<Vector2>(_input);

		protected override bool IMP_TryStartInteraction(Cursor cursor) => true;

		protected override bool IMP_TryPerpetuateInteraction(Cursor cursor, Vector2 drag)
		{
			var selfPosition = (Vector2)transform.position;

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
			_stick.transform.position = new Vector3(stickPosition.x, stickPosition.y, _stick.transform.position.z);

			return true;
		}

		protected override void EXT_EndInteraction(Cursor cursor)
		{
			var selfPosition = (Vector2)transform.position;
			
			_stick.transform.position = new Vector3(selfPosition.x, selfPosition.y, _stick.transform.position.z);
			
			if (_resetInputOnInteractionEnd)
				_input = Vector2.zero;
		}

		public override bool IMP_TryPerpetuateRecuperation() => false;
	}
}