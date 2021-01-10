using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using PhoneApp.Core.Builder;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;
using PhoneApp.Core.PageObject.NewEntry;
using PhoneApp.Tests.Model;

namespace PhoneApp.Tests.Scenario.NewEntry
{
	[TestFixture]
    public class NewEntry : BaseTests
    {

        private static string ErrorMsg = "The field cannot be empty";

	    private Contact TestContact { get; }

	    public NewEntry()
	    {
		    TestContact = ContactBuilder.WithDefault().Build();
        }

	    private void Init()
	    {
		    ModelHelper.RemoveContact(TestContact);
	    }


        [Test]
        [Description("Navigate to AddNewEntry page and create a new contact")]
        [Expected("The contact is correctly created")]
        public void CreateNewEntry()
        {
	        Init();
            CreateNewEntryWithSuccess(TestContact);
        }

        [Test]
        [Description("Navigate to AddNewEntry page and try to create an existing contact")]
        [Expected("The contact is not created")]
        public void CreateExistingEntry()
        {
	        RestApiHelper.CreateContacts(TestContact.ToJson());

           var newEntryPage = HomePage.AddNewEntryContainer.ClickOnAddNewEntryButton()
		        .FillForm(TestContact)
		        .ClickOnSubmit();

	        var msg = newEntryPage.GetFailureMsgAfterSubmit();
	        TestCase.Assert(msg, "An error occured!", "Check the error message is present");
        }


        [Test]
        [Description("Navigate to AddNewEntry page and create a new contact with an empty first name")]
        [Expected("The contact is created")]
        public void CreateNewEntryWithEmptyFirstName()
        {
	        Init();
            TestContact.FirstName = string.Empty;
            CreateNewEntryWithSuccess(TestContact);
        }


        [Test]
        [Description("Navigate to AddNewEntry page and try to create a new contact with an empty last name")]
        [Expected("The contact is not created")]
        public void CreateNewEntryWithEmptyLastName()
        {
	        Init();
            TestContact.LastName = string.Empty;
            AssertNewEntryWithFailure( TestContact, EntryEnum.LastName, ErrorMsg);
        }

        [Test]
        [Description("Navigate to AddNewEntry page and try to create a new contact with an empty country code")]
        [Expected("The contact is not created")]
        public void CreateNewEntryWithEmptyCountryCode()
        {
	        Init();
            TestContact.ContactPhones.First().CountryCode = string.Empty;
	        AssertNewEntryWithFailure(TestContact, EntryEnum.CountryCode, ErrorMsg);
        }

        [Test]
        [Description("Navigate to AddNewEntry page and try to create a new contact with an empty area code")]
        [Expected("The contact is not created")]
        public void CreateNewEntryWithEmptyAreaCode()
        {
	        Init();
            TestContact.ContactPhones.First().AreaCode = string.Empty;
	        AssertNewEntryWithFailure(TestContact, EntryEnum.AreaCode, ErrorMsg);
        }

        [Test]
        [Description("Navigate to AddNewEntry page and try to create a new contact with an empty phone number")]
        [Expected("The contact is not created")]
        public void CreateNewEntryWithEmptyPhoneNumber()
        {
	        Init();
            TestContact.ContactPhones.First().PhoneNumber = string.Empty;
	        AssertNewEntryWithFailure(TestContact, EntryEnum.PhoneNumber, ErrorMsg);
        }


        private void CreateNewEntryWithSuccess( Contact contact)
        {
	        var newEntryPage = HomePage.AddNewEntryContainer.ClickOnAddNewEntryButton()
		                                                    .FillForm(contact)
		                                                    .ClickOnSubmit();


	        var msg = newEntryPage.GetSuccessMsgAfterSubmit();
	        TestCase.Assert(msg, "Save was successful!", "Check the successful message is present");
        }

        private void AssertNewEntryWithFailure(Contact contact, EntryEnum targetField, string errorMessage )
        {
	        var newEntryPage = HomePage.AddNewEntryContainer.ClickOnAddNewEntryButton()
	                                                        .FillForm(contact)
	                                                        .ClickOnSubmit();

	        var locator     = AddNewEntryPage.LocatorDictionary[targetField];
	        var control     = newEntryPage.Browser.Driver.FindElement(By.Id(locator));
            var errorField  = control.FindElement(By.XPath("following-sibling::*"));
            var errorMsg    = errorField.Text;

            TestCase.Assert(errorMsg, errorMessage, "Check the error message");
            TestCase.Assert(errorField.GetAttribute("class"), "invalid-feedback", "Check the error attribute");
        }


    }
}