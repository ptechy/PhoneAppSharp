using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PhoneApp.Core.Context;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhoneApp.Tests.Api
{
	public static class ScenarioApi
	{
		public static TestSuiteReport GetFailedTestSuiteResult(this QaTestSuite testSuite, TestSuiteSettings settings, QaError error)
		{
			return  TestSuiteReport.Create(	testSuite.TestSuiteName,
											testSuite.StartDate,
											new List<TestCaseReport>(),
											settings,
											error);
		}

		public static TestCaseReport ToFailedTestCaseReport(this QaTestCase testCase, QaDetail detail, string message)
		{
			return TestCaseReport.Create(  testCase.TestCaseName,
										   testCase.StartDate,
										   detail,
										   testCase.StepLogger ?? new StepLogger(),
										   new QaError(message),
										   ExecutionStatus.Nok);
		}

		public static ProjectSettings GetReportSettings()
		{
			var basePath = TestContext.CurrentContext.WorkDirectory;

			var builder = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			var section = builder.GetSection("GlobalSettings");

			var baseUrl = section["BaseUrl"];
			var screenshotFolder = section["ScreenshotFolder"];
			var logFileName = section["LogFileName"];
			var reportFolderContainerPath = section["ReportBasePath"];
			var reportFolderName = section["ReportFolderName"];
			var reportName = section["ReportName"];

			var reportBasePath = Directory.Exists(reportFolderContainerPath)
				? reportFolderContainerPath
				: basePath;

			var reportFolderPath = Path.Combine(reportBasePath, reportFolderName);
			var reportFilePath = Path.Combine(reportFolderPath, reportName);


			return new ProjectSettings(baseUrl,
				screenshotFolder,
				reportFolderName,
				logFileName,
				reportName,
				reportFolderPath,
				reportFilePath);

		}

		public static TestSuiteReport ToTestSuiteReport(this QaTestSuite testSuite, IEnumerable<TestCaseReport> testCaseReports)
		{
			try
			{
				return  TestSuiteReport.Create(testSuite.TestSuiteName, testSuite.StartDate, testCaseReports, testSuite.Settings, null);
			}
			catch (Exception ex)
			{
				return testSuite.GetFailedTestSuiteResult(testSuite.Settings, new QaError(ex.Message));
			}
		}

		public static TestCaseReport ToTestCaseReport(this QaTestCase testCase,  TestContext context, Browser browser)
		{
			var executionStatus = TestApi.GetExecutionStatus(context.Result.FailCount, context.Result.InconclusiveCount);
			var qaDetail		= context.GetQaDetail();

			try
			{
				var error = executionStatus.GetError(browser, testCase.Settings, context.Result.Message);
				
				return TestCaseReport.Create( testCase.TestCaseName,
											  testCase.StartDate, 
											  qaDetail,
											  testCase.StepLogger,
											  error,
											  executionStatus);
			}
			catch (Exception ex)
			{
				return testCase.ToFailedTestCaseReport(qaDetail, ex.Message);
			}
		}

		public static QaError GetError(this ExecutionStatus status, Browser browser,  TestCaseSettings settings, string message )
		{
			if (status == ExecutionStatus.Ok)
				return null;

			browser.TakeScreenshot(settings);
			return new QaError(message, settings.ScreenshotHtmlPath);

		}

		public static QaDetail GetQaDetail(this TestContext context)
		{
			return new QaDetail( context.GetTestDescription(),
								 context.GetTestExpectedResult());
		}

		public static string GetTestDescription(this TestContext context)
		{
			if (!context.Test.Properties.ContainsKey("Description"))
				return "The test DOES NOT CONTAIN the Description attribute";
				

			return (string)context.Test.Properties["Description"].First();

		}

		public static string GetTestExpectedResult(this TestContext context)
		{
			if (!context.Test.Properties.ContainsKey("Expected"))
				return "The test method DOES NOT CONTAIN Expected attribute";

			return (string)context.Test.Properties["Expected"].First();
		}
		

	}
}
