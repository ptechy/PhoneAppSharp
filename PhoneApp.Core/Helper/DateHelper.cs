using System;
using System.Text;
using PhoneApp.Core.Exception;

namespace PhoneApp.Core.Helper
{
	public static class DateHelper
	{
		public static TimeSpan GetDuration(DateTime toDate, DateTime startDate)
		{
			if (startDate > toDate)
				throw new TestToolException($"startDate: {startDate.Date} is greater than ToDate: {toDate.Date}");
			return toDate - startDate;
		}

	}
}
