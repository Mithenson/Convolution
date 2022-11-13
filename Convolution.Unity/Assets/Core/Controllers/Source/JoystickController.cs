using UnityEngine;

using Cursor = Convolution.Handling.Cursor;

namespace Convolution.Controllers
{
	public class JoystickController : Controller
	{
		[SerializeField]
		private Transform _stick;

		[SerializeField]
		[Min(0.1f)]
		private float _extent;
		
		protected override bool IMP_TryStartInteraction(Cursor cursor) => true;

		protected override bool IMP_TryPerpetuateInteraction(Cursor cursor, Vector2 drag)
		{
			var delta = cursor.Position - Position;

			if (delta.magnitude > _extent)
				delta = delta.normalized * _extent;

			var stickPosition = Position + delta;
			_stick.transform.position = new Vector3(stickPosition.x, _stick.transform.position.y, stickPosition.y);

			return true;
		}

		protected override void EXT_EndInteraction(Cursor cursor) => _stick.transform.position = new Vector3(Position.x, _stick.transform.position.y, Position.y);

		public override bool IMP_TryPerpetuateRecuperation() => false;
	}
}