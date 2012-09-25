using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tee.Cmd.MediaManager
{
	public class SortableBindingList<T> : BindingList<T>
	{
		public SortableBindingList(IList<T> list) : base(list)
		{
		}

		protected override bool SupportsSortingCore
		{
			get { return true; }
		}

		protected override void ApplySortCore(PropertyDescriptor Property, ListSortDirection Direction)
		{
			var itemsList = (List<T>) Items;
			if (Property.PropertyType.GetInterface("IComparable") != null)
			{
				itemsList.Sort(delegate(T x, T y)
				               	{
				               		// Compare x to y if x is not null. If x is, but y isn't, we compare y
				               		// to x and reverse the result. If both are null, they're equal.

				               		if (Property.GetValue(x) != null)
				               		{
				               			var comparable = (IComparable) Property.GetValue(x);
				               			if (comparable != null)
				               				return comparable.CompareTo(Property.GetValue(y))*
				               				       (Direction == ListSortDirection.Descending ? -1 : 1);
				               		}

				               		if (Property.GetValue(y) != null)
				               		{
				               			var value = (IComparable) Property.GetValue(y);
				               			if (value != null)
				               				return value.CompareTo(Property.GetValue(x))*
				               				       (Direction == ListSortDirection.Descending ? 1 : -1);
				               		}

				               		return 0;
				               	});
			}
		}
	}
}