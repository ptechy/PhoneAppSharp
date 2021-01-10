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
	public class QaMeasure
	{
		public string Name { get; }
		public QaTestCounter TestCounter { get; }
		public TimerReport TimerReport { get; }
		public ExecutionStatus Status { get; }


		private QaMeasure(string name, QaTestCounter testCounter,  TimerReport timerReport, ExecutionStatus status)
		{
			Name			= name;
			TestCounter		= testCounter;
			TimerReport		= timerReport;
			Status			= status;
		}

		public static QaMeasure Create(  string name, QaTestCounter counter, DateTime startDate)
		{
			var status = counter.Failed == 0
						? ExecutionStatus.Ok
						: ExecutionStatus.Nok;

			return new QaMeasure(name,
								 counter,
								 new TimerReport(startDate, counter.Duration),
								 status);
		}
	}
}
