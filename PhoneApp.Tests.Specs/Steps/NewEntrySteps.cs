using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using PhoneApp.Core.Builder;
using PhoneApp.Core.Exception;
using PhoneApp.Core.Extension;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.PageObject.NewEntry;
using PhoneApp.Tests.Specs.Api;
using PhoneApp.Tests.Specs.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PhoneApp.Tests.Specs.Steps
{
	[Binding]
	public class NewEntrySteps : TechTalk.SpecFlow.Steps
	{
		private AddNewEntryPage CurrentPage { get; }
		private TestCaseContext TestCaseContext { get; }

		public NewEntrySteps(TestCaseContext testCaseContext)
		{
			TestCaseContext = testCaseContext;
			CurrentPage		= new AddNewEntryPage(TestCaseContext.Browser);
		}


		[When(@"I fill the form with all the test contact values")]
		public void WhenIFillTheFormWithTheTestContactValues()
		{
			CurrentPage.FillForm(TestCaseContext.TestContact);
		}

		[When(@"I click on the submit button")]
		public void WhenIClickOnTheSubmitButton()
		{
			CurrentPage.ClickOnSubmit();
		}

		[When(@"I click on the confirm button")]
		public void WhenIClickOnTheConfirmButton()
		{
			CurrentPage.ClickOnConfirm();
		}

		[Then(@"I check the contact is correctly created")]
		public void ThenICheckTheContactIsCorrectlyCreated()
		{
			var msg = CurrentPage.GetSuccessMsgAfterSubmit();
			TestCaseContext.TestCase.Assert(msg, "Save was successful!", "Check the successful message is present");
		}

		[Then(@"I check the contact is not created with an error message")]
		public void ThenICheckErrorWithMessage()
		{
			var expectedErrorMsg = "An error occured!";
			var msg				 = CurrentPage.GetFailureMsgAfterSubmit();
			TestCaseContext.TestCase.Assert(msg, expectedErrorMsg , "Check the error message is present");

		}

		[Then(@"I check the contact is not created with (.*) field error")]
		public void ThenICheckErrorWithFiledName(EntryEnum fieldName)
		{
			var expectedErrorMsg = "The field cannot be empty";
			var locator			 = AddNewEntryPage.LocatorDictionary[fieldName];
			var control			 = CurrentPage.Browser.Driver.FindElement(By.Id(locator));
			var errorField		 = control.FindElement(By.XPath("following-sibling::*"));
			var errorMsg		 = errorField.Text;
			
			TestCaseContext.TestCase.Assert(errorMsg, expectedErrorMsg, "Check the error message");
			TestCaseContext.TestCase.Assert(errorField.GetAttribute("class"), "invalid-feedback", "Check the error attribute");
		}



	}
}
