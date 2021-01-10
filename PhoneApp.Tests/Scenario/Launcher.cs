using NLog;
using NUnit.Framework;
using PhoneApp.Core.Report;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Api;
using System;

namespace PhoneApp.Tests.Scenario
{
	[SetUpFixture]
	public  static class Launcher
	{
		public static QaTestProject  TestProject { get; }

		static Launcher()
		{
			TestProject	= new QaTestProject("Test Project", 
											ScenarioApi.GetReportSettings());
		}


		[OneTimeSetUp]
		public static void BeforeRun()
		{
			TestProject.Settings.InitFolders()
								.UpdateLogger(LogLevel.Info);
		}

		[OneTimeTearDown]
		public static void AfterRun()
		{
			var testProjectReport = TestProjectReport.Create(	TestProject.ProjectName,
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
