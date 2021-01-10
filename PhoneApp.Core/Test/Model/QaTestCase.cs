using System;
using PhoneApp.Core.Context;

namespace PhoneApp.Core.Test.Model
{
	public class QaTestCase
	{
		public TestCaseSettings Settings { get; }
		public StepLogger StepLogger { get; }
		public DateTime StartDate { get; }
		public string TestCaseName { get; }

		public QaTestCase(string testCaseName, TestCaseSettings settings)
		{
			Settings = settings;
			StepLogger = new StepLogger();
			StartDate = DateTime.Now;
			TestCaseName = testCaseName;
		}

		public void Assert(string actual, string expected, string description)
		{
			StepLogger.Assert(actual, expected, description);
		}

		public void Assert(int actual, int expected, string description)
		{
			StepLogger.Assert(actual, expected, description);
		}
	}
}
