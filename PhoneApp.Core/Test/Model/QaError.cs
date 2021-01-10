namespace PhoneApp.Core.Test.Model
{
	public class QaError
	{
		public string Message { get; } 
		public string ScreenshotHtmlPath { get; }

		public QaError(string message, string screenshotHtmlPath)
		{
			Message = message;
			ScreenshotHtmlPath = screenshotHtmlPath;
		}

		public QaError(string message)
		{
			Message = message;
		}
	}
}
