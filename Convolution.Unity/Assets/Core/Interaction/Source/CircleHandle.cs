using Maxim.Common.Extensions;
using UnityEngine;

namespace Convolution.Interaction
{
	public sealed class CircleHandle : Handle
	{
		[SerializeField]
		[Min(0.1f)]
		private float _radius;

		public override bool IsHovered(Cursor cursor, out float separation)
		{
			var selfPosition = (Vector2)transform.position;

			separation = (cursor.Position - selfPosition).magnitude;
			return separation < cursor.Radius + _radius;
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