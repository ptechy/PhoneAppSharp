using System.Collections.Generic;
using OpenQA.Selenium.DevTools.V84.SystemInfo;
using PhoneApp.Core.Assertion;
using PhoneApp.Core.Exception;
using PhoneApp.Core.Test;
using PhoneApp.Core.Test.Model;

namespace PhoneApp.Core.Context
{
	public class StepLogger
	{
		public AssertionStatus Status { get; private set; }
		public List<string> Steps { get; }
		public List<AssertionCase> Assertions { get; }

		public StepLogger()
		{
			Steps = new List<string>();
			Assertions = new List<AssertionCase>();
		}
		public void AddStep(string text)
		{
			Steps.Add(text);
		}

		public void Assert(string actual, string expected, string description)
		{
			CheckAssertion(new AssertionCase(actual, expected, description));
		}

		public void Assert(int actual, int expected, string description)
		{
			CheckAssertion(new AssertionCase(actual, expected, description));
		}

		private void CheckAssertion(AssertionCase assertionCase)
		{
			Assertions.Add(assertionCase);

			if (assertionCase.Status != AssertionStatus.Nok)
				return;

			Status = AssertionStatus.Nok;
			throw new TestToolException("Assertion failed");

		}
	}


}
