using System;

namespace PhoneApp.Core.Test.Model
{
	public class QaTestSuite
	{
		public TestSuiteSettings Settings { get; }
		public DateTime StartDate { get; }
		public string TestSuiteName { get; }

		public QaTestSuite(string testSuiteName, TestSuiteSettings settings)
		{
			TestSuiteName = testSuiteName;
			StartDate = DateTime.Now;
			Settings = settings ;
		}
	}
}
