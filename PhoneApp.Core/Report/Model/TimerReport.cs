using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Core.Report.Model
{
	public class TimerReport
	{
		public string Date { get; }
		public string StartDate { get; }
		public string Duration { get; }

		public TimerReport(DateTime startDate, TimeSpan timeSpan)
		{
			Date = startDate.ToString("yyyy-MM-dd");
			StartDate = startDate.ToString("HH:mm:ss");
			Duration = timeSpan.ToString("m'm 's's'");
		}
	}
}
