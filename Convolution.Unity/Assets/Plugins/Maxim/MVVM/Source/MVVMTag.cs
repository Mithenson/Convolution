using System;
using Maxim.Common;
using Maxim.Common.Tags;

namespace Maxim.MVVM
{
	[Serializable]
	public sealed class MVVMTag : Tag
	{
		protected override int Id => default;
	}
}