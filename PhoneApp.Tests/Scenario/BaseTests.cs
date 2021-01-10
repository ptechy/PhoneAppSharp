using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NUnit.Framework;
using PhoneApp.Core.Context;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.Report;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Api;

namespace PhoneApp.Tests.Scenario
{
	[TestFixture]
    public abstract class BaseTests
    {
        private List<TestCaseReport> TestCaseReports { get; set; }
        protected Browser Browser { get; set; }
        protected HomePage HomePage { get; set; }
        protected QaTestSuite TestSuite { get; set; }
        protected QaTestCase TestCase { get; set; }



        [OneTimeSetUp]
        public void BeforeTestSuite()
        {
            TestSuite           = new QaTestSuite(GetType().Name, new TestSuiteSettings(Launcher.TestProject.Settings));
	        TestCaseReports     = new List<TestCaseReport>();
        }

        [SetUp]
        public void BeforeEachTest()
        {
            TestCase  = new QaTestCase(  TestContext.CurrentContext.Test.MethodName, new TestCaseSettings(TestSuite.Settings));
            Browser   = BrowserFactory.GetBrowser(BrowserType.Chrome, Launcher.TestProject.Settings.BaseUrl, TestCase.StepLogger)
	                                  .NavigateToHomepage();
            HomePage  = new HomePage(Browser);
        }


        [TearDown]
        public  void AfterEachTest()
        {
			var	testCaseReport = TestCase.ToTestCaseReport(TestContext.CurrentContext, Browser);
			TestCaseReports.Add(testCaseReport);
			Browser.Dispose();
			Assert.That(testCaseReport.Measure.Status, Is.EqualTo(ExecutionStatus.Ok));
        }

        [OneTimeTearDown]
        public void AfterTestSuite()
        {
	        Browser?.Dispose();
	        var testSuiteReport = TestSuite.ToTestSuiteReport(TestCaseReports);

	        try
			{
				testSuiteReport.GenerateReportForTestSuite(TestSuite.Settings);
			}
			catch (Exception ex)
			{
                Assert.Fail(ex.ToString());
			}

            Launcher.TestProject.TestSuiteReports.Add(testSuiteReport);
        }

    }
}