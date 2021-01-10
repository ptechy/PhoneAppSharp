using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Context
{
	public static class BrowserFactory
	{

		public static Browser GetBrowser(BrowserType browserType, string baseUrl, StepLogger stepLogger)
		{
			return browserType switch
			{
				BrowserType.Chrome => new Browser(GetChromeDriver(), BrowserType.Chrome, baseUrl, stepLogger),
				BrowserType.ChromeHeadless => new Browser(GetHeadlessChromeDriver(), BrowserType.ChromeHeadless, baseUrl, stepLogger),
				BrowserType.Firefox => new Browser(GetFirefoxDriver(), BrowserType.Firefox, baseUrl, stepLogger),
				_ => throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null)
			};
		}
		private static string GetDriverPath()
		{
			return  AppDomain.CurrentDomain.BaseDirectory;
		}

		private static IWebDriver GetChromeDriver()
		{
			return new ChromeDriver(new ChromeOptions());
		}

		private static IWebDriver GetHeadlessChromeDriver()
		{
			var options = new ChromeOptions();
			options.AddArguments("--headless");
			return new ChromeDriver( options);
		}
		private static IWebDriver GetFirefoxDriver()
		{
			var options = new FirefoxOptions();
			return new FirefoxDriver(GetDriverPath(), options);
		}
	}
}
