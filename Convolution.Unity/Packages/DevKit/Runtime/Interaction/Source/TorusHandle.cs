using Maxim.Common.Extensions;
using UnityEngine;

namespace Convolution.DevKit.Interaction
{
	public sealed class TorusHandle : Handle
	{
		[SerializeField]
		private float _radius;

		[SerializeField]
		private float _width;

		public override bool IsHovered(Cursor cursor, out float separation)
		{
			var selfPosition = (Vector2)transform.position;

			separation = Mathf.Abs((cursor.Position - selfPosition).magnitude - _radius);
			return separation < cursor.Radius + _width;
		}
		
		#if UNITY_EDITOR
		
		private void OnDrawGizmosSelected()       
		{
			var previousHandlesColor = UnityEditor.Handles.color;
			UnityEditor.Handles.color = Color.white.SetAlpha(0.25f);

			var innerRadiusWorldSpace = transform.position + Vector3.right * (_radius - _width * 0.5f);
			var innerRadiusGUI = UnityEditor.HandleUtility.WorldToGUIPointWithDepth(innerRadiusWorldSpace);
			var innerRadiusScreen = UnityEditor.HandleUtility.GUIPointToScreenPixelCoordinate(innerRadiusGUI);
			
			var outerRadiusWorldSpace = transform.position + Vector3.right * (_radius + _width * 0.5f);
			var outerRadiusGUI = UnityEditor.HandleUtility.WorldToGUIPointWithDepth(outerRadiusWorldSpace);
			var outerRadiusScreen = UnityEditor.HandleUtility.GUIPointToScreenPixelCoordinate(outerRadiusGUI);

			var thickness = (innerRadiusScreen - outerRadiusScreen).magnitude;
			UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _radius, thickness);

			UnityEditor.Handles.color = previousHandlesColor;
		}
		
		#endif
	}
}