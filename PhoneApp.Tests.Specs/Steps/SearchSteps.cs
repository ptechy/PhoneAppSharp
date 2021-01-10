using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PhoneApp.Core.Helper;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.PageObject.Search;
using PhoneApp.Tests.Specs.Model;
using TechTalk.SpecFlow;

namespace PhoneApp.Tests.Specs.Steps
{
	[Binding]
	public class SearchSteps : TechTalk.SpecFlow.Steps
	{
		private SearchResultPage CurrentPage { get; }

		public TestCaseContext TestCaseContext { get; set; }

		public SearchSteps(TestCaseContext testCaseContext)
		{
			TestCaseContext = testCaseContext;
			CurrentPage = new SearchResultPage(TestCaseContext.Browser);
		}

		[Then(@"Check the Search field is filled with (.*)")]
		public void ThenCheckTheSearchFieldIsFilledWith(string value)
		{
			var searchFiledContact = CurrentPage.SearchContainer.GetSearchFieldContent();
			TestCaseContext.TestCase.Assert(searchFiledContact, value, "assert value set in search field is equal to the value found");
		}

		[Then(@"Check the values of all contact found")]
		public void ThenCheckTheValuesOfAllContactFound()
		{
			var contacts = CurrentPage.GetSearchResultTable().ToList();
			var testContact = TestCaseContext.TestContact;
			TestCaseContext.TestCase.Assert(contacts.Count, testContact.ContactPhones.Count(), "assert number of phone are correct");

			// assert all lines
			var items = testContact.ContactPhones.Zip(contacts, (x, y) => new { Expected = x, Actual = y });
			foreach (var item in items)
			{
				TestCaseContext.TestCase.Assert(testContact.FirstName, item.Actual.NameContainer.FirstName, "Check first name");
				TestCaseContext.TestCase.Assert(testContact.LastName, item.Actual.NameContainer.LastName, "Check last name");

				var expectedPhoneNumber = ModelHelper.GetPhoneNumberFormat(	item.Expected.CountryCode,
																			item.Expected.AreaCode,
																			item.Expected.PhoneNumber);

				TestCaseContext.TestCase.Assert(expectedPhoneNumber, item.Actual.PhoneNumberContainer.FullPhoneNumber, "Check phone number");
			}
		}

	}
}
