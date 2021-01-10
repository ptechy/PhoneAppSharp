using System.Linq;
using NUnit.Framework;
using PhoneApp.Core.Builder;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;
using PhoneApp.Tests.Model;

namespace PhoneApp.Tests.Scenario.Search
{
	[TestFixture]
    public class NewSearchTests : BaseTests
    {
	    private Contact TestContact { get; }


		public NewSearchTests()
	    {
			TestContact = ContactBuilder.WithDefault().Build();
		}

		private void Init()
		{
			ModelHelper.RemoveContact(TestContact);
		}


		[Test]
		[Description("Navigate to search page and search for an existing contact")]
		[Expected("The contact is found")]
		public void SearchForExistingContactWithLastName()
        {
	        Init();
	        RestApiHelper.CreateContacts(TestContact.ToJson());
			SearchForExistingContact("COLOGNE", "COLOGNE");
        }


		[Test]
		[Description("Navigate to search page and launch a search that returns no result")]
        [Expected("The search returns no result")]
		public void SearchWithNoResult()
		{
			var fieldValue = "ZZZZZZZZZZZZZZZZZ";
			HomePage.SearchContainer.SetValueInSearchField(fieldValue);

	        var searchResultPage	= HomePage.SearchContainer.ClickOnGoButton();
	        var contacts			= searchResultPage.GetSearchResultTable().ToList();
	        var searchFiledContact	= searchResultPage.SearchContainer.GetSearchFieldContent();
	        var columnNames			= searchResultPage.GetTableColumns();

	        TestCase.Assert(searchFiledContact,fieldValue, "assert contact name is equal to the search name");
	        TestCase.Assert(contacts.Count, 0, "check number of contact found");
	        TestCase.Assert(columnNames.Count, 4, "check number of columns displayed");
	        TestCase.Assert(columnNames[0],"Contact Name", "check contact name field");
	        TestCase.Assert(columnNames[1], "Phone Number", "check phone number field");
	        TestCase.Assert(columnNames[2], string.Empty, "check third column is empty");
	        TestCase.Assert(columnNames[3], string.Empty, "check fourth column is empty");
        }


        [Test]
        [Description("Navigate to search page and search with an existing phone number")]
        [Expected("The phone number is found")]
		public void SearchByPhoneNumberAnExistingContact()
		{
			Init();
			RestApiHelper.CreateContacts(TestContact.ToJson());

			var fieldValue = "+35 117 999998";
			SearchForExistingContact(TestContact.LastName, fieldValue);
        }


		private void SearchForExistingContact(string contactName, string fieldValue)
        {
	        var dbContact = ModelHelper.GetRestApiContacts(contactName).First();

	        HomePage.SearchContainer.SetValueInSearchField(fieldValue);

	        var searchResultPage	= HomePage.SearchContainer.ClickOnGoButton();
	        var contacts			= searchResultPage.GetSearchResultTable().ToList();
	        var searchFiledContact	= searchResultPage.SearchContainer.GetSearchFieldContent();

	        TestCase.Assert(searchFiledContact, fieldValue, "assert value set in search field is equal to the value found");
	        TestCase.Assert(contacts.Count, dbContact.ContactPhones.Count(), "assert number of phone are correct");

	        // assert all lines
	        var items = dbContact.ContactPhones.Zip(contacts, (x, y) => new { Expected = x, Actual = y });
	        foreach (var item in items)
	        {
		        TestCase.Assert(dbContact.FirstName, item.Actual.NameContainer.FirstName, "Check first name");
		        TestCase.Assert(dbContact.LastName, item.Actual.NameContainer.LastName, "Check last name");

		        var expectedPhoneNumber = ModelHelper.GetPhoneNumberFormat( item.Expected.CountryCode, 
																			item.Expected.AreaCode, 
																			item.Expected.PhoneNumber);

		        TestCase.Assert(expectedPhoneNumber, item.Actual.PhoneNumberContainer.FullPhoneNumber, "Check phone number");
	        }

        }

    }
}