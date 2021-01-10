using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Core.Context;
using PhoneApp.Core.Model;
using PhoneApp.Core.PageObject;
using PhoneApp.Core.PageObject.Home;
using PhoneApp.Core.Test.Model;
using TechTalk.SpecFlow;

namespace PhoneApp.Tests.Specs.Model
{
	public class TestCaseContext : SpecFlowContext
	{
		public QaDetail Detail { get; set; }
		public  Browser Browser { get; set; }
		public  QaTestCase TestCase { get; set; }
		public Contact  TestContact { get; set; }

	}
}
