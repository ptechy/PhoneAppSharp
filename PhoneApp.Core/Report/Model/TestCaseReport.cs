using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Core.Context;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Report.Model
{
	public class TestCaseReport
	{
		public QaMeasure Measure { get; }
		public QaDetail Detail { get; }
		public StepLogger StepLogger { get; }
		public QaError Error { get; }

		private TestCaseReport(QaMeasure measure, QaDetail detail, StepLogger stepLogger, QaError error)
		{
			Measure = measure;
			Detail = detail;
			StepLogger = stepLogger;
			Error = error;
		}

		public static TestCaseReport Create(string testCaseName, DateTime startDate, QaDetail detail, StepLogger stepLogger, QaError error, ExecutionStatus status)
		{
			var duration	= DateHelper.GetDuration(DateTime.Now, startDate);
			var testCounter = new QaTestCounter(1,
												(int)status,
												1 - (int)status,
												0,
												duration);

			var measure = QaMeasure.Create(testCaseName, testCounter, startDate);

			return new TestCaseReport(measure, detail, stepLogger, error);
		}
	}
}
