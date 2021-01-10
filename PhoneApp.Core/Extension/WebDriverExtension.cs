using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PhoneApp.Core.Extension
{
	public static class WebDriverExtension
	{

        /// WaitForCompleteLoading will wait the complete loading of the page
        /// NoSuchElementException if the element is not found  
        public static void WaitForCompleteLoading(this IWebDriver driver, int timeoutInSeconds = 10)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInSeconds));
			wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
		}


		/// Wait for element to be found,
		/// NoSuchElementException if the element is not found  
        public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeoutInSeconds = 10)
		{
			return new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds)).Until(d => d.FindElement(@by));
		}


        /// Waits for an element to be displayed
        /// NoSuchElementException if the element is not found  
        public static IWebElement WaitForElementVisible(this IWebDriver driver, By by, int timeoutInSeconds = 5)
        {
            new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds)).Until(d => d.FindElement(by).Displayed);
            Thread.Sleep(500);
            return driver.FindElement(by);
        }

        public static IWebElement WaitForElementEnabled(this IWebDriver driver, By by, int timeoutInSeconds = 5)
        {
	        new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds)).Until(d => d.FindElement(by).Enabled);
	        Thread.Sleep(500);
	        return driver.FindElement(by);
        }
























    }
}
