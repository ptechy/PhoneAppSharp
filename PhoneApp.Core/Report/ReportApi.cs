using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using PhoneApp.Core.Exception;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model.Search;
using PhoneApp.Core.Report.Model;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using Encoding = System.Text.Encoding;

namespace PhoneApp.Core.Report
{
	public static  class ReportApi
	{
		public static void GenerateReportForTestProject(this TestProjectReport  project)
		{
			var resourceFile	= Encoding.UTF8.GetString(Properties.Resources.TestProjectTemplate);
			var content			= project.TestSuiteReports.GetHtmlContent(project.Measure, resourceFile, "project");

			project.Settings.ReportFilePath.CreateHtmlFile(content);
		}
		
		public static void GenerateReportForTestSuite(this TestSuiteReport testSuiteReport,  TestSuiteSettings settings)
		{
			var resourceFile = Encoding.UTF8.GetString(Properties.Resources.TestSuiteTemplate);
			var content		 = new List<TestSuiteReport>{ testSuiteReport }.GetHtmlContent(testSuiteReport.Measure, resourceFile, "testsuite");

			settings.TestSuiteFilePath.CreateHtmlFile(content);
		}


		public static string GetHtmlContent(this IEnumerable<TestSuiteReport> testSuiteReports, QaMeasure measure, string resourceFile, string templateKey)
		{
			try
			{
				return Engine.Razor.RunCompile(resourceFile, templateKey, null, new { Measure = measure, TestSuiteReports = testSuiteReports });
			}
			catch (System.Exception ex)
			{
				throw new TestToolException($"REPORT ERROR: {ex.Message}");
			}
		}


		private static void CreateHtmlFile(this string path, string content)
		{
			path.CreateFile(content.Replace("TEST_STYLES", Properties.Resources.styles));
		}

	}
}
