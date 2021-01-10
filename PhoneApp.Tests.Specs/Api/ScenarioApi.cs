using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PhoneApp.Core.Context;
using PhoneApp.Core.Model;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;
using PhoneApp.Tests.Specs.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PhoneApp.Tests.Specs.Api
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


			return new ProjectSettings( baseUrl,
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

		public static TestCaseReport ToTestCaseReport(this TestCaseContext testCaseContext,  TestContext context)
		{
			var executionStatus = TestApi.GetExecutionStatus(context.Result.FailCount, context.Result.InconclusiveCount);

			try
			{
				var error = executionStatus.GetError(testCaseContext.Browser, testCaseContext.TestCase.Settings, context.Result.Message);
				
				return TestCaseReport.Create( testCaseContext.TestCase.TestCaseName,
											  testCaseContext.TestCase.StartDate,
											  testCaseContext.Detail,
											  testCaseContext.TestCase.StepLogger,
											  error,
											  executionStatus);
			}
			catch (Exception ex)
			{
				return testCaseContext.TestCase.ToFailedTestCaseReport(testCaseContext.Detail, ex.Message);
			}
		}

		public static QaError GetError(this ExecutionStatus status, Browser browser,  TestCaseSettings settings, string message )
		{
			if (status == ExecutionStatus.Ok)
				return null;

			browser.TakeScreenshot(settings);
			return new QaError(message, settings.ScreenshotHtmlPath);

		}
		
		public static IEnumerable<Contact> ToContact(this Table table)
		{
			return 	table.CreateSet<TempContact>()
					.GroupBy(t => new { First = t.FirstName, Last = t.LastName })
					.Select(s =>
					{
						return new Contact
						{
							FirstName = s.Key.First,
							LastName = s.Key.Last,
							ContactPhones = s.ToList()
								.Select(p => new ContactPhone
								{
									CountryCode = p.CountryCode,
									AreaCode = p.AreaCode,
									PhoneNumber = p.PhoneNumber
								}).ToList()
						};
					});
		}
		



	}
}
