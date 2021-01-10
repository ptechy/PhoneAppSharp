using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneApp.Core.Model.Search
{
	public class NameContainer
	{
		public string FirstName { get; }
		public string LastName { get; }

		public NameContainer (IEnumerable<string> values)
		{
			if (!values.Any())
				throw new ArgumentException("Last name should not be empty");

			FirstName = values.Count() == 1
				? string.Empty
				: values.First();

			LastName = values.Last();
		}


	}
}
