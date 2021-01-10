using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using PhoneApp.Core.Context;
using PhoneApp.Core.Extension;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model.Search;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.PageObject.NewEntry;

namespace PhoneApp.Core.PageObject.Search
{
	public class SearchResultPage :BasePage
	{
		private static string DeleteConfirm			= "//button[contains(.,'Yes, delete')]";
		private static string AbortDeletion			= "//button[contains(.,'No, return')]";
		private static string AssertDeletion		= "//div[contains(.,'The operation was successful')]";
		private static string TableClassContainer	= "table";

		public AddNewEntryContainer AddNewEntryContainer { get; }
		public SearchContainer SearchContainer { get; }

		public SearchResultPage(Browser browser) : base(browser)
		{
			browser.StepLogger.AddStep("Wait for SearchPage to be loaded");
			SearchContainer			= new SearchContainer(browser);
			AddNewEntryContainer	= new AddNewEntryContainer(browser);
		}

		public List<string> GetTableColumns()
		{
			return GetResultTableContainer().GetTableColumns().ToList();
		} 
		public IWebElement GetResultTableContainer()
		{
			return Browser.Driver.WaitForElementEnabled(By.ClassName(TableClassContainer));
		}

		public List<TableRow> GetSearchResultTable ()
		{
			return GetResultTableContainer().GetResultTable().ToList();
		}

		public AddNewEntryPage ClickOnEditLink(int index)
		{
			Browser.StepLogger.AddStep("Click on edit link");
			GetLine(index).EditLink.Click();
			return new AddNewEntryPage(Browser);
		}

		public AddNewEntryPage ClickOnEditIcon(int index)
		{
			Browser.StepLogger.AddStep("Click on edit icon");
			GetLine(index).EditImage.Click();
			return new AddNewEntryPage(Browser);
		}

		public SearchResultPage ClickOnDeleteIcon(int index)
		{
			Browser.StepLogger.AddStep("Click on delete icon");
			GetLine(index).DeleteButton.Click();
			return this;
		}


		public SearchResultPage ClickOnDeleteButton(int index)
		{
			Browser.StepLogger.AddStep("Click on delete button");
			GetLine(index).DeleteButton.Click();
			return this;
		}

		public SearchResultPage ConfirmDeletion()
		{
			Browser.StepLogger.AddStep("Click on deletion");
			Browser.Driver.WaitForElementVisible(By.XPath(DeleteConfirm)).Click();
			Browser.StepLogger.AddStep("Wait for deletion confirmation");
			Browser.Driver.WaitForElementVisible(By.XPath(AssertDeletion));
			return this;
		}

		public SearchResultPage CancelDeletion()
		{
			Browser.StepLogger.AddStep("Cancel deletion");
			Browser.Driver.WaitForElementVisible(By.XPath(AbortDeletion)).Click();
			return this;
		}

		private TableRow GetLine( int index)
		{
			var currentResultTable = GetSearchResultTable();
			index--;
			return   currentResultTable[index];
		}
	}
}
