using System.IO;
using PhoneApp.Core.Exception;

namespace PhoneApp.Core.Helper
{
	public static class FileHelper
	{
		public static void CreateDir(this string dirFullPath)
		{
			try
			{
				if (!Directory.Exists(dirFullPath))
					Directory.CreateDirectory(dirFullPath);
			}
			catch (System.Exception ex)
			{
				throw new TestToolException($"Dir:{dirFullPath} IS NOT CREATED => {ex}");
			}
		}

		public static void CreateFile(this string fileFullPathName, string fileContent)
		{
			try
			{
				if (!File.Exists(fileFullPathName))
					Directory.CreateDirectory(new FileInfo(fileFullPathName).Directory?.FullName!);
				
				File.WriteAllText(fileFullPathName, fileContent);
			}
			catch (System.Exception ex)
			{
				throw new TestToolException($"File:{fileFullPathName} IS NOT CREATED => {ex} ");
			}
		}

		public static void DeleteDirectory( this string directoryPath)
		{
			try
			{
				if (Directory.Exists(directoryPath))
					Directory.Delete(directoryPath, true);
			}
			catch (System.Exception ex)
			{
				throw new TestToolException($"Directory:{directoryPath} IS NOT DELETED => {ex} ");
			}
		}

	}
}
