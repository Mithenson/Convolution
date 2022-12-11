using UnityEngine;

namespace VirtCons.Internal.Application.Common.Source
{
	public interface IApplicationService
	{
		Vector2Int GetMousePosition();
	}

	public sealed class NullApplicationService : IApplicationService
	{
		public Vector2Int GetMousePosition() => default;
	}
}