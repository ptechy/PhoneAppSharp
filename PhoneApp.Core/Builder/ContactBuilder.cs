using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;

namespace PhoneApp.Core.Builder
{
	public class ContactBuilder
	{
		private Contact Contact { get; }

		private  ContactBuilder()
		{
			Contact = new Contact();
		}

		public static  ContactBuilder WithDefault()
		{
			return new ContactBuilder()
				.WithFirstName("Aude")
				.WithLastName("COLOGNE")
				.WithContactPhones(new List<ContactPhone>
				{
					PhoneBuilder.WithDefault().Build()
				});
		}

		public static  ContactBuilder WithRandomValues()
		{
			return new ContactBuilder()
				.WithFirstName(GetRandom.Name(5))
				.WithLastName(GetRandom.Name(5, false))
				.WithContactPhones(new List<ContactPhone>
				{
					PhoneBuilder.WithRandomValues().Build()
				});
		}

		public ContactBuilder WithContactId(int id)
		{
			Contact.ContactId = id;
			return this;
		}

		public ContactBuilder WithFirstName(string firstName)
		{
			Contact.FirstName = firstName;
			return this;
		}


		public ContactBuilder WithLastName(string lastName)
		{
			Contact.LastName = lastName;
			return this;
		}

		public ContactBuilder WithContactPhones(IEnumerable<ContactPhone> contactPhones)
		{
			Contact.ContactPhones = contactPhones;
			return this;
		}

		public Contact Build()
		{
			return Contact;
		}
	}
}
