using System;
using System.Reflection;
using UnityEngine;

namespace Maxim.Common
{
	[Serializable]
	public struct AssemblyReference
	{
		[SerializeField]
		private string _firstTypeAssemblyQualifiedName;

		public Assembly Value => Type.GetType(_firstTypeAssemblyQualifiedName).Assembly;
	}
}