using OpenQA.Selenium;
using PhoneApp.Core.Context;
using PhoneApp.Core.Extension;
using PhoneApp.Core.PageObject.NewEntry;

namespace PhoneApp.Core.PageObject.Home
{
	public class AddNewEntryContainer 
	{
		private static string AddNewEntryXpathButton = "//button[@class='btn btn-primary btn-md']";
		private IWebElement AddNewEntryButtonElement { get; }
		protected Browser Browser { get; }

		public AddNewEntryContainer(Browser browser)
		{
			Browser		= browser;
			AddNewEntryButtonElement = Browser.Driver.WaitForElement(By.XPath(AddNewEntryXpathButton));
		}

		public AddNewEntryPage ClickOnAddNewEntryButton()
		{
			Browser.StepLogger.AddStep("Click on Add New Entry button");
			AddNewEntryButtonElement.Click();
			return new AddNewEntryPage(Browser);
		}
	}
}
