using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PhoneApp.Core.Report;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Specs.Api;
using TechTalk.SpecFlow;

namespace PhoneApp.Tests.Specs.Hooks
{
	[Binding]
	public class FeatureHook
	{
		public static  QaTestSuite TestSuite { get; private set; }
		public static List<TestCaseReport> TestCaseReports { get; private set; }




		[BeforeFeature]
		public static void BeforeFeature(FeatureContext featureContext)
		{
			TestSuite = new QaTestSuite(featureContext.FeatureInfo.Title, new TestSuiteSettings(TestProjectHook.TestProject.Settings));
			TestCaseReports = new List<TestCaseReport>();

		}

		[AfterFeature]
		public static void AfterFeature(FeatureContext featureContext)
		{
			var testSuiteReport = TestSuite.ToTestSuiteReport(TestCaseReports);

			try
			{
				testSuiteReport.GenerateReportForTestSuite(TestSuite.Settings);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}

			TestProjectHook.TestProject.TestSuiteReports.Add(testSuiteReport);

		}
	}
}
