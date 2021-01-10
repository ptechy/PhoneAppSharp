using PhoneApp.Core.Context;
using PhoneApp.Core.PageObject.Home;

namespace PhoneApp.Core.PageObject
{
	public class BasePage
	{
		public Browser Browser { get;  }
		public HeaderContainer Header { get; }

		public BasePage(Browser browser)
		{
			Browser = browser;
			Header = new HeaderContainer(browser);
		}
	}
}
