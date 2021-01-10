using System.Collections.Generic;

namespace PhoneApp.Core.Model
{
	public class Contact
	{
		public int ContactId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public IEnumerable<ContactPhone> ContactPhones { get; set; }
	}
}
