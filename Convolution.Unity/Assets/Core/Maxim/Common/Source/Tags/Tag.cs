using System;

namespace Maxim.Common.Tags
{
	public abstract class Tag : IEquatable<Tag>
	{
		protected abstract int Id { get; }
        
		public static bool operator ==(Tag left, Tag right) => Equals(left, right);
		public static bool operator !=(Tag left, Tag right) => !Equals(left, right);
        
		public override bool Equals(object obj) => Equals(obj as Tag);
		public bool Equals(Tag other)
		{
			if (ReferenceEquals(null, other) || other.GetType() != GetType())
				return false;

			return Id == other.Id;
		}
        
		public override int GetHashCode()
		{
			unchecked
			{
				return (GetType().GetHashCode() * 397) ^ Id.GetHashCode();
			}
		}
	}
}