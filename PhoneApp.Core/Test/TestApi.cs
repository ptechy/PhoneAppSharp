using NLog;
using NLog.Config;
using NLog.Targets;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Test.Model;
using System;
using System.IO;

namespace PhoneApp.Core.Test
{
	public static  class TestApi
	{
		public static ProjectSettings InitFolders(this ProjectSettings settings)
		{
			settings.BaseFolderFullPath.DeleteDirectory();
			settings.BaseFolderFullPath.CreateDir();
			return settings;
		}


		public static ProjectSettings UpdateLogger(this ProjectSettings settings, LogLevel logLevel)
		{
			var logName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss-ffff}_test.log";
			var loggerFullpathName = Path.Combine(new[]
			{
				settings.BaseFolderFullPath,
				logName
			});

			var consoleTarget = new ColoredConsoleTarget
			{
				Name = "ColoredConsole",
				Layout = @"${longdate}   ${message}"
			};

			var fileTarget = new FileTarget
			{
				FileName = loggerFullpathName,
				Layout = @"${longdate}   ${message}"
			};

			var config = new LoggingConfiguration();
			config.AddTarget("console", consoleTarget);
			config.AddTarget("file", fileTarget);

			config.LoggingRules.Add(new LoggingRule("*",
				logLevel,
				consoleTarget));

			config.LoggingRules.Add(new LoggingRule("*",
				logLevel,
				fileTarget));

			LogManager.Configuration = config;
			return settings;
		}




		public static ExecutionStatus GetExecutionStatus(int failed, int inconclusive)
		{
			return 	failed == 0 && inconclusive == 0
					? ExecutionStatus.Ok
					: ExecutionStatus.Nok;

		}
	}
}
