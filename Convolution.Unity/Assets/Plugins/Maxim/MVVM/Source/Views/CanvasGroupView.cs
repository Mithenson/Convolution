using UnityEngine;

namespace Maxim.MVVM.Views
{
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasGroupView : MonoBehaviour
	{
		private bool _isActive;
		public bool IsActive
		{
			get => _isActive;
			set
			{
				_group ??= GetComponent<CanvasGroup>();
				
				if (value)
				{
					_group.blocksRaycasts = true;
					_group.alpha = 1.0f;
					
					_isActive = true;
				}
				else
				{
					_group.blocksRaycasts = false;
					_group.alpha = _offOpacity;
					
					_isActive = false;
				}
			}
		}
		
		[SerializeField]
		[Range(0.0f, 1.0f)]
		private float _offOpacity = 0.5f;
		
		private CanvasGroup _group;

		private void Awake() => _group ??= GetComponent<CanvasGroup>();
	}
}