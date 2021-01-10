using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Report.Model
{
	public class TestProjectReport
	{
		public string ProjectName { get; }
		public QaMeasure Measure { get; }
		public ProjectSettings Settings { get; }

		public IEnumerable<TestSuiteReport> TestSuiteReports { get; }

		private TestProjectReport(string projectName, QaMeasure measure, ProjectSettings settings, IEnumerable<TestSuiteReport> testSuiteReports)
		{
			ProjectName = projectName;
			Measure = measure;
			Settings = settings;
			TestSuiteReports = testSuiteReports;
		}


		public static TestProjectReport Create(string projectName, DateTime startDate, IEnumerable<TestSuiteReport> testsuiteReports, ProjectSettings settings)
		{
			var testCounter = testsuiteReports.Select(t => t.Measure.TestCounter).Aggregate((result, item) => result + item);
			var measure		= QaMeasure.Create(projectName, testCounter, startDate);

			return new TestProjectReport(projectName, measure,  settings, testsuiteReports);
		}
	}
}
