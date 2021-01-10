using System;
using System.IO;

namespace PhoneApp.Core.Test.Model
{
	public class TestSuiteSettings
	{
		public string FolderName { get; }
		public string TestSuitePath { get; }
		public string TestSuiteFilePath { get; }
		public TestSuiteSettings(ProjectSettings settings)
		{
			FolderName = $"{DateTime.Now:yyyy-MM-dd-HHmmssffff}";
			TestSuitePath = Path.Combine(settings.BaseFolderFullPath, FolderName);
			TestSuiteFilePath = Path.Combine(TestSuitePath, "index.html");
		}
	}
}
