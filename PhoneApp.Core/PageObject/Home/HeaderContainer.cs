using OpenQA.Selenium;
using PhoneApp.Core.Context;
using PhoneApp.Core.Extension;

namespace PhoneApp.Core.PageObject.Home
{
	public class HeaderContainer 
	{
		private static string HomeXPathLink = "//a[@Class='text-dark nav-link']";
		private static string PhoneBookWebAppClassLink = "navbar-brand";
		protected Browser Browser { get; }
		protected StepLogger StepLogger { get; }
		public IWebElement HomeLinkElement { get; }
		public IWebElement PhoneBookWebAppLinkElement { get; }

		public HeaderContainer(Browser browser) 
		{
			Browser = browser;
			PhoneBookWebAppLinkElement = Browser.Driver.WaitForElementVisible(By.ClassName(PhoneBookWebAppClassLink));
			HomeLinkElement = Browser.Driver.WaitForElementVisible(By.XPath(HomeXPathLink));
		}

		public HomePage ClickOnHomeLink()
		{
			Browser.StepLogger.AddStep("Click on Home link");
			HomeLinkElement.Click();
			return new HomePage(Browser);
		}

		public HomePage ClickOnPhoneBookWebAppLink()
		{
			Browser.StepLogger.AddStep("Click on PhoneBookWebApp link");
			PhoneBookWebAppLinkElement.Click();
			return new HomePage(Browser);
		}
	}
}
