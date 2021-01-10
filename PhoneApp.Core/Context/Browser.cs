using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using PhoneApp.Core.Exception;
using PhoneApp.Core.Extension;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Context
{
	public class Browser 
	{
		public StepLogger StepLogger { get; }
		private string BaseUrl { get; set; }
		public  BrowserType BrowserType { get;  }
		public IWebDriver Driver { get; }
		private string MainWindowHandle { get; set; }
		private List<string> Handles { get; }

		public Browser( IWebDriver driver, BrowserType browserType, string baseUrl, StepLogger stepLogger)
		{
			StepLogger = stepLogger;
			Driver = driver;
			BaseUrl = baseUrl;
			BrowserType = browserType;
			Handles = new List<string>();
		}

		public Browser SetBaseUrl(string baseUrl)
		{
			BaseUrl = baseUrl;
			return this;
		}

		public void Dispose()
		{
			StepLogger.AddStep("Dispose browser");
			try
			{
				Driver.Close();
				Driver.Dispose();
			}
			catch (System.Exception)
			{
			}
		}

		public Browser Navigate(string baseUrl)
		{
			StepLogger.AddStep("Navigate to base url");
			BaseUrl = baseUrl;
			return StartTestAt(BaseUrl);
		}

		public Browser NavigateToHomepage()
		{
			StepLogger.AddStep("Navigate to homepage");
			return StartTestAt(BaseUrl);
		}
		public Browser StartTestAt(string url)
		{
			Driver.Navigate().GoToUrl(url);
			Driver.WaitForCompleteLoading();
			Driver.Manage().Window.Maximize();
			MainWindowHandle = Driver.CurrentWindowHandle;
			Handles.Add(MainWindowHandle);
			return this;
		}

		public string OpenNewTab()
		{
			StepLogger.AddStep("Open new tab");
			((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");
			return GetNewTab();
		}


		public string ClickAndOpenNewTab(string link)
		{
			var handle = OpenNewTab();
			SwitchToTab(handle);
			Driver.Navigate().GoToUrl(link);
			return handle;
		}

		public string ClickAndOpenNewTab(By locator)
		{
			return ClickAndOpenNewTab(Driver.FindElement(locator));
		}

		public string ClickAndOpenNewTab(IWebElement element)
		{
			if(element.GetAttribute("href") == null)
				throw new ArgumentException("Error: it is not a link, Href does not exist");

			var action = new Actions(Driver);
			action.KeyDown(Keys.Control).MoveToElement(element).Click().Perform();
			return GetNewTab();
		}


		private string GetNewTab()
		{
			var newHandles = Driver.WindowHandles.ToList()
				.Where( h => !Handles.Contains(h));

			var currentTab = newHandles.Last();
			Handles.Add(currentTab);
			return currentTab;
		}

		public void SwitchToMainTab()
		{
			Driver.SwitchTo().Window(MainWindowHandle);
		}

		public void SwitchToTab(string handle)
		{
			StepLogger.AddStep("Switch to another tab");
			Driver.SwitchTo().Window(handle);
		}

		public void SwitchToTab()
		{
			Driver.SwitchTo().Window(Handles.Last());
		}

		public void CloseCurrentTab()
		{
			StepLogger.AddStep("Close tab");
			CloseTab(Handles.Last());
		}

		public void CloseTab(string handle)
		{
			SwitchToTab(handle);
			Driver.Close();
			RemoveHandle(handle);
		}


		public void TakeScreenshot(TestCaseSettings settings)
		{
			StepLogger.AddStep("Take screenshot");
			try
			{
				Driver.Manage().Window.Size = new Size(1024, 768);
				settings.ScreenshotFolderPath.CreateDir();
				var screenshot = (Driver as ITakesScreenshot)?.GetScreenshot();
				screenshot.SaveAsFile(settings.ScreenshotFilePath, ScreenshotImageFormat.Png); // Format values are Bmp, Gif, Jpeg, Png,
			}
			catch (System.Exception ex)
			{
				throw new TestToolException($"Error with screenshot:{ex}");
			}

		}
		private void RemoveHandle(string handle)
		{
			if( !Handles.Contains(handle))
				throw new TestToolException($"Error: handle does not exist => {handle}");

			var all = Driver.WindowHandles.ToList().Where(h => h.Contains(handle));
			if(all.Any())
				throw new TestToolException($"Error: tab is not closed => {handle}");

			Handles.Remove(handle);
		}
	}
}
