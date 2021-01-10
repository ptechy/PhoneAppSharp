using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Report.Model
{
	public class TestSuiteReport 
	{
		public QaMeasure Measure { get; }
		public TestSuiteSettings Settings { get; }
		public IEnumerable<TestCaseReport> TestCaseReports { get; }

		public QaError  Error { get; }

		private TestSuiteReport(QaMeasure measure,  IEnumerable<TestCaseReport> testCaseReportList, TestSuiteSettings settings,  QaError error)
		{
			Measure = measure;
			TestCaseReports = testCaseReportList;
			Settings = settings;
			Error = error;
		}


		public static TestSuiteReport Create(string testSuiteName, DateTime startDate, IEnumerable<TestCaseReport> testCaseReports, TestSuiteSettings settings, QaError error)
		{
			var testCounter		= testCaseReports.Select( t=> t.Measure.TestCounter).Aggregate((result, item) => result + item);
			var measure			=  QaMeasure.Create(testSuiteName, testCounter, startDate);

			return new TestSuiteReport(measure, testCaseReports, settings, error);
		}
	}
}
