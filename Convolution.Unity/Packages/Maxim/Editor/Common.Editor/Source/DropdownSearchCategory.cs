using System;
using System.Collections.Generic;
using System.Linq;

namespace Maxim.Common.Editor
{
	public class DropdownSearchCategory<T>
	{
		public string Name { get; private set; }
		public IEnumerable<T> Values => _values;

		#region Fields

		private List<T> _values;
		private List<DropdownSearchCategory<T>> _subCategories;

		#endregion
        
		public DropdownSearchCategory(string name)
		{
			Name = name;
            
			_subCategories = new List<DropdownSearchCategory<T>>();
			_values = new List<T>();
		}

		#region Members

		public void Add(T value) => _values.Add(value);
		public void Add(DropdownSearchCategory<T> subCategory) => _subCategories.Add(subCategory);
        
		public bool TryGet(string subSource, out DropdownSearchCategory<T> output)
		{
			var splitSubSource = subSource.Split('/').ToList();
			splitSubSource.Insert(0, Name);
            
			return TryGet(splitSubSource.ToArray(), 0, out output);
		}
		private bool TryGet(string[] splitSubSource, int index, out DropdownSearchCategory<T> output)
		{
			if (splitSubSource[index] == Name)
			{
				if (index == splitSubSource.Length - 1)
				{
					output = this;
					return true;
				}
                
				foreach (var subCategory in _subCategories)
				{
					if (!subCategory.TryGet(splitSubSource, index + 1, out output)) continue;
					return true;
				}

				index++;
				var previous = this;
				var current = new DropdownSearchCategory<T>(splitSubSource[index]);
                
				previous.Add(current);

				for (var i = index + 1; i < splitSubSource.Length; i++)
				{
					previous = current;
					current = new DropdownSearchCategory<T>(splitSubSource[i]);
                    
					previous.Add(current);
				}

				output = current;
				return true;
			}

			output = null;
			return false;
		}
        
		public void Relay(Action<DropdownSearchCategory<T>,int> predicate, int depth, bool use = true)
		{
			if (use)
				predicate(this, depth);
	        
			foreach (var category in _subCategories) 
				category.Relay(predicate, depth + 1);
		}

		#endregion
	}
}