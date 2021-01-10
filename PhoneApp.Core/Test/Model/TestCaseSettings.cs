using System;
using System.IO;

namespace PhoneApp.Core.Test.Model
{
	public class TestCaseSettings
	{
		public string ScreenshotHtmlPath { get; }
		public string ScreenshotFolderPath { get; }
		public string ScreenshotFilePath { get; }


		public TestCaseSettings(TestSuiteSettings settings)
		{
			var folder				= $"{DateTime.Now:yyyy-MM-dd-HHmmssffff}";
			var screenshotName		= "screen.png";
			ScreenshotHtmlPath		= $"./{folder}/{screenshotName}";
			ScreenshotFolderPath	=  Path.Combine(settings.TestSuitePath, folder);
			ScreenshotFilePath		= Path.Combine(ScreenshotFolderPath, screenshotName);
		

		}
	}
}
