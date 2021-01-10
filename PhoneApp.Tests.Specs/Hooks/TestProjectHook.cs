using System;
using NLog;
using NUnit.Framework;
using PhoneApp.Core.Report;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Specs.Api;
using TechTalk.SpecFlow;

namespace PhoneApp.Tests.Specs.Hooks
{
	[Binding]
    public sealed class TestProjectHook
    {
	    public static QaTestProject TestProject { get; }


        static TestProjectHook()
        {
	        TestProject = new QaTestProject("Test Project",
											ScenarioApi.GetReportSettings());
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
	        TestProject.Settings.InitFolders()
		        .UpdateLogger(LogLevel.Info);
		}


        [AfterTestRun]
        public static void AfterTestRun()
        {
	        var testProjectReport = TestProjectReport.Create(TestProject.ProjectName,
															 TestProject.StartDate,
															 TestProject.TestSuiteReports,
															 TestProject.Settings);
	        try
	        {
		        testProjectReport.GenerateReportForTestProject();
	        }
	        catch (Exception ex)
	        {
		        Assert.Fail(ex.ToString());
	        }
        }



    }
}