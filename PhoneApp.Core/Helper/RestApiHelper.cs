using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PhoneApp.Core.Exception;
using PhoneApp.Core.Model;

namespace PhoneApp.Core.Helper
{

	public static  class RestApiHelper
	{
		private static string ContactsUrl = "http://wemanityphonebook.azurewebsites.net/api/Contacts";

		public static string GetUrl(string contactName)
		{
			return $"{ContactsUrl}?searchQuery={contactName}";
		}

		public static string RemoveUrl(int contactId)
		{
			return $"{ContactsUrl}/{contactId}";
		}

		public static string CreateContacts( string json)
		{
			var request = (HttpWebRequest)WebRequest.Create(ContactsUrl);
			request.Timeout = 20000;
			var encoding = new ASCIIEncoding();
			var bytesToWrite = encoding.GetBytes(json);
			request.Method = "POST";
			request.ContentLength = bytesToWrite.Length;
			request.ContentType = "application/json";
			var newStream = request.GetRequestStream();
			newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
			newStream.Close();

			var resp = (HttpWebResponse)request.GetResponse();
			var dataStream = resp.GetResponseStream();

			return new StreamReader(dataStream).ReadToEnd();
		}

		public static void RemoveContacts(string name)
		{
			var existingContacts = ModelHelper.GetRestApiContacts(name).ToList();
			foreach (var contact in existingContacts)
			{
				 var removeUrl = RemoveUrl(contact.ContactId);
				 var response = DeleteRequest(removeUrl);

				 if(!response.ToLower().Equals("no content"))
					 throw  new TestToolException($"Server Error when deleting user {contact.LastName}");
			}
		}

		public static string GetRequest(string url)
		{
			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);
				var response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode != HttpStatusCode.OK)
					return null;

				var receiveStream = response.GetResponseStream();
				var readStream = response.CharacterSet == null
					? new StreamReader(receiveStream)
					: new StreamReader(receiveStream, Encoding.UTF8);

				var webData = readStream.ReadToEnd();
				response.Close();
				readStream.Close();
				return webData;
			}
			catch
			{
				return null;
			}
		}

		public static string DeleteRequest(string url)
		{
			var request = WebRequest.Create(url);
			request.Method = "DELETE";
			var response = (HttpWebResponse)request.GetResponse();
			return response.StatusDescription;
		}
	}
}
