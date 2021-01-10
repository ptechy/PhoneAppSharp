using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium.DevTools.V87.DOM;
using PhoneApp.Core.Context;
using PhoneApp.Core.Exception;
using PhoneApp.Core.Helper;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Specs.Api;
using PhoneApp.Tests.Specs.Hooks;
using PhoneApp.Tests.Specs.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PhoneApp.Tests.Specs.Steps
{
	[Binding]
	public class HomeSteps : TechTalk.SpecFlow.Steps
	{

		private HomePage CurrentPage { get; set; }
		private  TestCaseContext TestCaseContext { get; }

		public HomeSteps(TestCaseContext  testCaseContext)
		{
				TestCaseContext = testCaseContext;
		}


		[Given(@"The test specifications")]
		public void GivenTheTestSpecifications(Table table)
		{
			TestCaseContext.Detail = table.CreateInstance<QaDetail>();
		}


		[Given(@"I use the following test contact")]
		[Given(@"I update the test contact")]
		public void GivenIUseTheFollowingTestContact(Table table)
		{
			TestCaseContext.TestContact = table.ToContact().First();
		}

		[Given(@"I remove the test contact inside the RestApi")]
		public void GivenIRemoveInsideTheRestApiTheDefaultTestContact()
		{
			if (TestCaseContext.TestContact == null)
				throw new TestToolException("Error: Test Contact must be initialized");

			ModelHelper.RemoveContact(TestCaseContext.TestContact);
		}

		[Given(@"I create a test contact inside the RestApi")]
		public void GivenICreateATestContactInsideTheRestApi()
		{
			GivenIRemoveInsideTheRestApiTheDefaultTestContact();
			RestApiHelper.CreateContacts(TestCaseContext.TestContact.ToJson());
		}

		[Given(@"I create the test contact with the RestApi")]
		public void GivenIcreateATestContact(Table table)
		{
			TestCaseContext.TestContact = table.ToContact().First();
			GivenICreateATestContactInsideTheRestApi();
		}


		[When(@"I navigate to homepage")]
		public void WhenINavigateToHomepage()
		{
			TestCaseContext.Browser.NavigateToHomepage();
			CurrentPage = new HomePage(TestCaseContext.Browser);

		}

		[When(@"I click on PhoneBookWebApp link")]
		public void WhenIClickOnPhoneBookWebAppLink()
		{
			CurrentPage.Header.ClickOnPhoneBookWebAppLink();
		}

		[When(@"I click on the home link")]
		public void WhenIClickOnTheHomeLink()
		{
			CurrentPage.Header.ClickOnHomeLink();
		}

		[When(@"I click on te AddNewEntry button")]
		public void WhenIClickOnTeAddNewEntryButton()
		{
			CurrentPage.AddNewEntryContainer.ClickOnAddNewEntryButton();
		}

		[When(@"I fill the search field with the value (.*)")]
		public void WhenIFillTheSearchFieldWithASpecificValue(string value)
		{
			CurrentPage.SearchContainer.SetValueInSearchField(value);
		}

		[When(@"I click on the Go button")]
		public void WhenIClickOnTheGoButton()
		{
			CurrentPage.SearchContainer.ClickOnGoButton();
		}

		[Then(@"I check the PhoneBookApp link is correct")]
		public void ThenICheckThePhoneBookAppLink()
		{
			TestCaseContext.TestCase.Assert(CurrentPage.Header.PhoneBookWebAppLinkElement.Text, "PhoneBookWebApp", "Check phone book link is present");
		}
		

		[Then(@"I check the Home link is correct")]
		public void ThenICheckTheHomeLink()
		{
			TestCaseContext.TestCase.Assert(CurrentPage.Header.HomeLinkElement.Text, "Home", "Check phone book link is present");
		}


	}
}
