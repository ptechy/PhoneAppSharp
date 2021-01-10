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
using PhoneApp.Core.PageObject.Search;
using PhoneApp.Tests.Specs.Api;
using PhoneApp.Tests.Specs.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PhoneApp.Tests.Specs.Steps
{
	[Binding]
	public class EditExistingEntrySteps : TechTalk.SpecFlow.Steps
	{
		private SearchResultPage CurrentPage { get; }
		private TestCaseContext TestCaseContext { get; }

		public EditExistingEntrySteps(TestCaseContext testCaseContext)
		{
			TestCaseContext = testCaseContext;
			CurrentPage		= new SearchResultPage(TestCaseContext.Browser);
		}



		[Then(@"I check the number of line is equals to (.*)")]
		public void ThenGivenIClickOnTheEditLink(int value)
		{
			TestCaseContext.TestCase.Assert(CurrentPage.GetSearchResultTable().Count, value, "Check the number of contact found");
		}

		[When(@"I click on the edit link")]
		public void GivenIClickOnTheEditLink()
		{
			CurrentPage.ClickOnEditLink(1);
		}

		[When(@"I click on the edit icon")]
		public void GivenIClickOnTheEditIcon()
		{
			CurrentPage.ClickOnEditIcon(1);
		}

		[When(@"I click on the delete button")]
		public void GivenIClickOnTheDeleteButton()
		{
			CurrentPage.ClickOnDeleteButton(1);
		}

		[When(@"I click on the delete icon")]
		public void GivenIClickOnTheDeleteIcon()
		{
			CurrentPage.ClickOnDeleteIcon(1);
		}

		[When(@"I confirm the deletion")]
		public void GivenIConfirmTheSuppresion()
		{
			CurrentPage.ConfirmDeletion();
		}

		[Then(@"I check all the test contact values are correct")]
		public void ThenICheckAllTestContactValues()
		{
			var contactValues = TestCaseContext.TestContact.GetFormValues();
			foreach (var control in AddNewEntryPage.LocatorDictionary)
			{
				var actual		= TestCaseContext.Browser.Driver.WaitForElementVisible(By.Id(control.Value)).GetAttribute("value");
				var expected	= contactValues[control.Key];
				TestCaseContext.TestCase.Assert(actual, expected, $"Assert field: {control.Key}");
			}
		}


	}
}
