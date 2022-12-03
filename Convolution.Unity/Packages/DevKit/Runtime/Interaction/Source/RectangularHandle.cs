using Maxim.Common.Extensions;
using UnityEngine;

namespace Convolution.DevKit.Interaction
{
	public sealed class RectangularHandle : Handle
	{
		[SerializeField]
		private Vector2 _size;

		public override bool IsHovered(Cursor cursor, out float separation)
		{
			var selfPosition = (Vector2)transform.position;
			
			var rotatedCursorPosition = (Vector2)(Quaternion.Inverse(transform.rotation) * cursor.Position);
			var delta = rotatedCursorPosition - selfPosition;

			separation = delta.magnitude;
			return Mathf.Abs(delta.x) < _size.x * 0.5f + cursor.Radius && Mathf.Abs(delta.y) < _size.y * 0.5f + cursor.Radius;
		}

		#if UNITY_EDITOR

		private void OnDrawGizmosSelected()
		{
			var extent = (Vector3)(_size * 0.5f);
			var verts = new Vector3[4]
			{
				transform.position - transform.right * extent.x - transform.up * extent.y,
				transform.position - transform.right * extent.x + transform.up * extent.y,
				transform.position + transform.right * extent.x + transform.up * extent.y,
				transform.position + transform.right * extent.x - transform.up * extent.y,
			};
			UnityEditor.Handles.DrawSolidRectangleWithOutline(verts, Color.white.SetAlpha(0.25f), Color.clear);
		}

		#endif
	}
}