using System;
using System.Linq;

namespace PhoneApp.Core.Model.Search
{
	public class PhoneNumberContainer
	{
		public string PhoneCode { get; }
		public string CountryCode { get;  }
		public string AreaCode { get; }
		public string PhoneNumber { get; }

		public string FullPhoneNumber { get; }

		public PhoneNumberContainer(string value )
		{
			FullPhoneNumber = value;
			var values		= value.Split().ToList();

			if (values.Count != 3)
				throw new ArgumentException("Phone number contact should have 4 entries");

			PhoneCode		= values[0].Substring(0, 1);
			CountryCode		= values[0].Substring(1); 
			AreaCode		= values[1];
			PhoneNumber		= values[2];
		}

	}
}
