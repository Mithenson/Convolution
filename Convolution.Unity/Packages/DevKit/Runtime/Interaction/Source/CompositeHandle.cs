using UnityEngine;

namespace Convolution.DevKit.Interaction
{
	public sealed class CompositeHandle : Handle
	{
		[SerializeField]
		private Handle[] _handles;

		public override bool IsHovered(Cursor cursor, out float separation)
		{
			var minSeparation = default(float?);
			foreach (var handle in _handles)
			{
				if (!handle.IsHovered(cursor, out var candidateSeparation))
					continue;

				if (!minSeparation.HasValue || candidateSeparation < minSeparation.Value)
					minSeparation = candidateSeparation;
			}

			if (!minSeparation.HasValue)
			{
				separation = default;
				return false;
			}

			separation = minSeparation.Value;
			return true;
		}
	}
}