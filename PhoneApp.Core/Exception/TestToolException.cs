using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Core.Exception
{
	public class TestToolException : System.Exception
	{
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

		public static string TestError = "TEST_ERROR:";
		private static string Error = string.Empty;



		public TestToolException() : base()
		{

		}

		public TestToolException(string message) : base(message)
		{
			Logger.Info($"{TestError} {message}");
			Error = message;
		}

		public TestToolException(string message, System.Exception ex) : base(message, ex)
		{
			Logger.Info($"{TestError} {message}");
			Logger.Info(ex.ToString);
			Error = $"{message}{ex}";
		}

		public static string GetError()
		{
			return Error;
		}
    }
}
