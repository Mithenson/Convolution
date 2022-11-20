using System;

namespace Maxim.Common
{
	public class SearchPathAttribute : Attribute
	{
		public SearchPathAttribute(string value) => Value = value;
        
		public string Value { get; private set; }
	}
}