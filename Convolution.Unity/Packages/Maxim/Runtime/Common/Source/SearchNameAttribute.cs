using System;

namespace Maxim.Common
{
	public class SearchNameAttribute : Attribute
	{
		public SearchNameAttribute(string value) => Value = value;
        
		public string Value { get; private set; }
	}
}