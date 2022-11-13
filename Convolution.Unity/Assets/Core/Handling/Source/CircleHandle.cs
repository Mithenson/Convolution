using UnityEngine;

namespace Convolution.Handling
{
	public sealed class CircleHandle : Handle
	{
		[SerializeField]
		[Min(0.1f)]
		private float _radius;

		public override bool IsHovered(Cursor cursor, out float separation)
		{
			var selfPosition = new Vector2(transform.position.x, transform.position.z);

			separation = (cursor.Position - selfPosition).magnitude;
			return separation < cursor.Radius + _radius;
		}

		#if UNITY_EDITOR
		
		private void OnDrawGizmosSelected() => UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, _radius);
		
		#endif
	}
}