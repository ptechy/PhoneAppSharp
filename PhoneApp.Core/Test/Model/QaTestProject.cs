using System;
using System.Collections.Generic;
using PhoneApp.Core.Report.Model;

namespace PhoneApp.Core.Test.Model
{
	public class QaTestProject
	{
		public string ProjectName { get; }
		public ProjectSettings Settings   { get; }
		public DateTime StartDate { get; }
		public List<TestSuiteReport> TestSuiteReports { get; }

		public QaTestProject(string projectName, ProjectSettings settings)
		{
			ProjectName = projectName;
			Settings = settings;
			StartDate = DateTime.Now;
			TestSuiteReports = new List<TestSuiteReport>();
		}
	}
}
