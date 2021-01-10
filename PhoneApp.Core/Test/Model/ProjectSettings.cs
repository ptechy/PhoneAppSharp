using System.IO;

namespace PhoneApp.Core.Test.Model
{
	public class ProjectSettings
	{
		public string BaseUrl { get; }
		public string ScreenshotFolder { get; }
		public string ReportFolderName { get; }
		public string LogFileName { get; }
		public string ReportName { get; }
		public string BaseFolderFullPath { get; }
		public string ReportFilePath { get;  }


		public ProjectSettings( string baseUrl, string screenshotFolder, string reportFolderName, string logFileName,
			string reportName, string baseFolderFullPath,string reportFilePath)
		{
			BaseUrl = baseUrl;
			ScreenshotFolder = screenshotFolder;
			ReportFolderName = reportFolderName;
			LogFileName = logFileName;
			ReportName = reportName;
			BaseFolderFullPath = baseFolderFullPath;
			ReportFilePath = Path.Combine(BaseFolderFullPath, reportName);
		}
	}
}
