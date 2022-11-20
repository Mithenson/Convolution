using Convolution.DevKit.Interaction;
using UnityEngine;
using Cursor = Convolution.DevKit.Interaction.Cursor;
using Grid = Convolution.DevKit.Placement.Grid;

namespace Convolution.DevKit.Controllers
{
	public abstract class Controller : MonoBehaviour
	{
		private void Awake()
		{
			Handle = GetComponentInChildren<Handle>();
			Grid = GetComponent<Grid>();
			
			EXT_Awake();
		}
		protected virtual void EXT_Awake() { }
		
		public ushort InputChannel { get; set; }
		public ControllerState State { get; private set; }
		public Handle Handle { get; private set; }
		public Grid Grid { get; private set; }

		private void Update()
		{
			if (State != ControllerState.Recuperating)
				return;
			
			if (!IMP_TryPerpetuateRecuperation())
				State = ControllerState.AtRest;
		}
		protected virtual void EXT_Update() { }

		public abstract IControllerInput ComputeInput();

		public bool TryStartInteraction(Cursor cursor)
		{
			if (IMP_TryStartInteraction(cursor))
			{
				State = ControllerState.BeingInteractedWith;
				return true;
			}
			else
			{
				return false;
			}
		}
		protected abstract bool IMP_TryStartInteraction(Cursor cursor);

		public void Interact(Cursor cursor, Vector2 drag)
		{
			if (IMP_TryPerpetuateInteraction(cursor, drag))
				return;
			
			StartRecuperation();
		}
		protected abstract bool IMP_TryPerpetuateInteraction(Cursor cursor, Vector2 drag);

		public void EndInteraction(Cursor cursor)
		{
			EXT_EndInteraction(cursor);
			StartRecuperation();
		}
		protected virtual void EXT_EndInteraction(Cursor cursor) { }

		private void StartRecuperation()
		{
			State = ControllerState.Recuperating;
			if (IMP_TryPerpetuateRecuperation())
				return;
			
			State = ControllerState.AtRest;
		}
		public abstract bool IMP_TryPerpetuateRecuperation();
	}
}
