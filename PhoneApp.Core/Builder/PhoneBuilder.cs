using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;

namespace PhoneApp.Core.Builder
{
	public class PhoneBuilder
	{
		private ContactPhone ContactPhone { get; }

		public PhoneBuilder()
		{
			ContactPhone = new ContactPhone();
		}

		public static PhoneBuilder WithDefault()
		{
			return new PhoneBuilder()
						.WithCountryCode(33)
						.WithAreaCode(112)
						.WithPhoneNumber(999999);
		}

		public static PhoneBuilder WithRandomValues()
		{
			return new PhoneBuilder()
				.WithCountryCode(GetRandom.RandomNumber(2))
				.WithAreaCode(GetRandom.RandomNumber(3))
				.WithPhoneNumber(GetRandom.RandomNumber(8));
		}

		public PhoneBuilder WithContactPhoneId(int id)
		{
			ContactPhone.ContactPhoneId = id;
			return this;
		}

		public PhoneBuilder WithCountryCode(int  countryCode)
		{
			ContactPhone.CountryCode = countryCode.ToString();
			return this;
		}

		public PhoneBuilder WithAreaCode(int areaCode)
		{
			ContactPhone.AreaCode = areaCode.ToString();
			return this;
		}

		public PhoneBuilder WithPhoneNumber(int phoneNumber)
		{
			ContactPhone.PhoneNumber = phoneNumber.ToString();
			return this;
		}

		public ContactPhone Build()
		{
			return ContactPhone;
		}
	}
}
