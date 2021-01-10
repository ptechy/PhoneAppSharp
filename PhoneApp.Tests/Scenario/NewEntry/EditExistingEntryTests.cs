using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using PhoneApp.Core.Builder;
using PhoneApp.Core.Extension;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;
using PhoneApp.Core.PageObject.NewEntry;
using PhoneApp.Tests.Model;

namespace PhoneApp.Tests.Scenario.NewEntry
{
	[TestFixture]
    public class EditExistingEntryTests : BaseTests
    {
	    private Contact TestContact { get; }

		public EditExistingEntryTests()
	    {
		    TestContact = ContactBuilder.WithDefault().Build();
		}

		private void Init()
		{
			ModelHelper.RemoveContact(TestContact);
			RestApiHelper.CreateContacts(TestContact.ToJson());
		}



	    [Test]
	    [Description("Navigate to search page, search an existing contact and edit the contact with the html link")]
	    [Expected("All the contact values are correct")]
		public void SearchExistingContactAndEditWithLink()
        {
	        Init();
	        HomePage.SearchContainer.SetValueInSearchField(TestContact.LastName);
			HomePage.SearchContainer.ClickOnGoButton()
									.ClickOnEditLink(1);
	        AssertEditFields(TestContact);
        }

        [Test]
        [Description("Navigate to search page, search an existing contact and edit the contact with the edit icon")]
        [Expected("All the contact values are correct")]
		public void SearchExistingContactAndEditWithIcon()
        {
	        Init();
	        HomePage.SearchContainer.SetValueInSearchField(TestContact.LastName);
	        HomePage.SearchContainer.ClickOnGoButton()
									.ClickOnEditIcon(1);
	        AssertEditFields(TestContact);
		}

        [Test]
        [Description("Navigate to search page, search an existing contact, edit the contact and change the first name")]
        [Expected("The new first name is taken into account")]
		public void SearchExistingContactAndEditAndChangeFirstName()
        {
	        Init();

			HomePage.SearchContainer.SetValueInSearchField(TestContact.LastName);

	        var editPage = HomePage.SearchContainer.ClickOnGoButton()
		        .ClickOnEditLink(1);

			var anotherContact = ContactBuilder.WithDefault()
											   .WithFirstName("ZZZZZZ")
											   .Build();

	        editPage.FillForm(anotherContact)
					.ClickOnSubmit()
					.ClickOnConfirm();

	        TestCase.Assert(editPage.GetSuccessMsgAfterSubmit(), "Save was successful!", "Check the successful message");
        }

        [Test]
        [Description("Navigate to search page, search for an existing contact and delete it with the delete icon")]
        [Expected("The contact is correctly deleted")]
		public void SearchExistingContactAndDeleteWithDeleteIcon()
        {
	        Init();
	        HomePage.SearchContainer.SetValueInSearchField(TestContact.LastName);
	        var searchResultPage = HomePage.SearchContainer.ClickOnGoButton()
														   .ClickOnDeleteIcon(1)
														   .ConfirmDeletion();

			TestCase.Assert(searchResultPage.GetSearchResultTable().Count(), 0, "Check the number of contact found");

        }


		[Test]
		[Description("Navigate to search page, search for an existing contact and delete it with the delete button")]
		[Expected("The contact is correctly deleted")]
		public void SearchExistingContactAndDeleteWithDeleteButton()
		{
			Init();
			HomePage.SearchContainer.SetValueInSearchField(TestContact.LastName);
			var searchResultPage = HomePage.SearchContainer.ClickOnGoButton()
														   .ClickOnDeleteButton(1)
														   .ConfirmDeletion();
			TestCase.Assert(searchResultPage.GetSearchResultTable().Count, 0, "Check the number of contact found");
		}

		private void AssertEditFields(Contact contact)
		{
			var contactValues = contact.GetFormValues();
			foreach (var control in AddNewEntryPage.LocatorDictionary)
			{
				var actual = Browser.Driver.WaitForElementVisible(By.Id(control.Value)).GetAttribute("value");
				var expected = contactValues[control.Key];
				TestCase.Assert(actual, expected,  "Assert field");
			}
		}
	}
}