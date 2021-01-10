using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium;
using PhoneApp.Core.Model;
using PhoneApp.Core.Model.Search;

namespace PhoneApp.Core.Helper
{
	public static class ModelHelper
	{
		public static string ToJson(this Contact contact)
		{
			return JsonConvert.SerializeObject(contact);
		}

		public static IEnumerable<Contact> GetRestApiContacts(string contactName)
		{
			var url = RestApiHelper.GetUrl(contactName);
			return GetContacts(url).ToList();
		}

		public static IEnumerable<Contact> GetContacts(string url)
		{
			var resp = RestApiHelper.GetRequest(url);
			return JsonConvert.DeserializeObject<List<Contact>>(resp).Where(t => t != null);
		}

		public static void RemoveContacts(IEnumerable<Contact> contacts)
		{
			foreach (var contact in contacts)
			{
				RestApiHelper.RemoveContacts(contact.LastName);
			}
		}

		public static void RemoveContact(Contact contact)
		{
			RestApiHelper.RemoveContacts(contact.LastName);
		}

		public static string GetPhoneNumberFormat(string countryCode, string areaCode, string phoneNumber)
		{
			return $"+{countryCode} {areaCode} {phoneNumber}";
		}


		public static  Dictionary<EntryEnum, string> GetFormValues(this Contact contact)
		{
			return new Dictionary<EntryEnum, string>
			{
				{EntryEnum.FirstName,   contact.FirstName},
				{EntryEnum.LastName,   contact.LastName},
				{EntryEnum.CountryCode,  contact.ContactPhones.First().CountryCode},
				{EntryEnum.AreaCode,    contact.ContactPhones.First().AreaCode},
				{EntryEnum.PhoneNumber,   contact.ContactPhones.First().PhoneNumber}
			};
		}
	}
}
