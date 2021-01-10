using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PhoneApp.Core.Context;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.Report;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Specs.Api;
using PhoneApp.Tests.Specs.Model;
using TechTalk.SpecFlow;

namespace PhoneApp.Tests.Specs.Hooks
{
	[Binding]
	public sealed class ScenarioHook 
	{

		public TestCaseContext TestCaseContext { get; set; }

		public ScenarioHook( TestCaseContext testCaseContext)
		{
			TestCaseContext = testCaseContext;
		}



		[BeforeScenario]
		public void BeforeScenario()
		{
			TestCaseContext.TestCase = new QaTestCase( TestContext.CurrentContext.Test.MethodName,
													   new TestCaseSettings(FeatureHook.TestSuite.Settings));

			TestCaseContext.Browser = BrowserFactory
				.GetBrowser(BrowserType.Chrome, TestProjectHook.TestProject.Settings.BaseUrl, TestCaseContext.TestCase.StepLogger);

		}

		[AfterScenario]
		public void AfterScenario()
		{
			var testCaseReport = TestCaseContext.ToTestCaseReport(TestContext.CurrentContext);
			FeatureHook.TestCaseReports.Add(testCaseReport);
			TestCaseContext.Browser.Dispose();
			Assert.That(testCaseReport.Measure.Status, Is.EqualTo(ExecutionStatus.Ok));
		}





	}
}
