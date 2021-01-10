using PhoneApp.Core.Context;

namespace PhoneApp.Core.PageObject.Home
{
	public class HomePage : BasePage
	{

		public AddNewEntryContainer AddNewEntryContainer { get; }
		public SearchContainer SearchContainer { get; }

		public HomePage(Browser browser) : base(browser)
		{
			browser.StepLogger.AddStep("Wait for homepage to be loaded");
			AddNewEntryContainer = new AddNewEntryContainer(browser);
			SearchContainer = new SearchContainer(browser);
		}

	}
}