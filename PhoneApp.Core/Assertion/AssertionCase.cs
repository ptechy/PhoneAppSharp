using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Assertion
{
	public class AssertionCase
	{
		public string Description { get; }
		public string Actual { get; }
		public string Expected { get; }
		public AssertionStatus Status { get; }


		public AssertionCase( string actual, string expected, string description)
		{
			Description = description;
			Actual		= actual ?? string.Empty;
			Expected	= expected;

			if (!Actual.ToLower().Equals(Expected.ToLower()))
				Status = AssertionStatus.Nok;
		}

		public AssertionCase(int actual, int expected, string description)
		{
			Description = description;
			Actual = actual.ToString();
			Expected = expected.ToString();

			if (actual != expected)
				Status = AssertionStatus.Nok;
		}

	}
}
