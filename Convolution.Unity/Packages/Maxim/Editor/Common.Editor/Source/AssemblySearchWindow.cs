using System.Reflection;

namespace Maxim.Common.Editor
{
	public sealed class AssemblySearchWindow : ValueSearchWindow<Assembly>
	{
		public override string GetNameFor(Assembly value) => value.GetName().Name;

		protected override bool TryGetOverrideNameFor(Assembly value, out string name)
		{
			name = default;
			return false;
		}
		protected override bool TryGetCategoryNameFor(Assembly value, out string categoryName)
		{
			categoryName = default;
			return false;
		}
	}
}