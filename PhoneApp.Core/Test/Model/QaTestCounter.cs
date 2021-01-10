using System;

namespace PhoneApp.Core.Test.Model
{
	public readonly struct QaTestCounter
	{
		public int Total { get; }
		public int Failed { get; } 

		public int Passed { get; }

		public int Ignored { get; }

		public TimeSpan Duration { get; }


		public QaTestCounter( int total,  int passed, int failed, int ignored, TimeSpan duration)
		{
			Total = total;
			Failed = failed;
			Passed = passed;
			Ignored = ignored;
			Duration = duration;
		}

		public static QaTestCounter operator +(QaTestCounter a, QaTestCounter b)
		{
			return new QaTestCounter( 
									a.Total + b.Total,
									a.Passed + b.Passed,
									a.Failed + b.Failed,
									a.Ignored + b.Ignored,
									a.Duration + b.Duration);
		}
	}
}
