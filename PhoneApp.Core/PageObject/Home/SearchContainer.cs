using OpenQA.Selenium;
using PhoneApp.Core.Context;
using PhoneApp.Core.Extension;
using PhoneApp.Core.PageObject.Search;

namespace PhoneApp.Core.PageObject.Home
{
	public class SearchContainer 
	{

		private static string SearchFieldId = "searchQuery";
		private static string GoButtonId = "searchButton";

		private IWebElement SearchFieldElement { get; }
		private IWebElement GoButtonElement { get; }
		protected Browser Browser { get; }
		protected StepLogger StepLogger { get; }

		public SearchContainer(Browser browser) 
		{
			Browser				= browser;
			SearchFieldElement	= Browser.Driver.WaitForElementVisible(By.Id(SearchFieldId));
			GoButtonElement		= Browser.Driver.WaitForElementVisible(By.Id(GoButtonId));
		}


		public void SetValueInSearchField(string value)
		{
			Browser.StepLogger.AddStep("Clear value in search field");
			SearchFieldElement.Clear();
			Browser.StepLogger.AddStep($"Set value in search field: {value}");
			SearchFieldElement.SendKeys(value);
			SearchFieldElement.Click();
		}

		public SearchResultPage ClickOnGoButton()
		{
			Browser.StepLogger.AddStep("Click on Go button");
			GoButtonElement.Click();
			return new SearchResultPage(Browser);
		}

		public string GetSearchFieldContent()
		{
			return Browser.Driver.WaitForElementVisible(By.Id(SearchFieldId)).GetAttribute("value");
		}
	}
}
