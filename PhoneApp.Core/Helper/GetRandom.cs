using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Core.Helper
{
	public static class GetRandom
	{
		public const string		PREFIX			= "TQA";
		private static string	LETTERS			= "abcdefghijklmnopqrstuvwxyz";
		private static string	ALPHANUMERICS	= "abcdefghijklmnopqrstuvwxyz0123456789";

		private static readonly Random Random	= new Random();


		private static long GetRandomValues()
		{
			return (long)Math.Round(Random.NextDouble() * (99999999 - 11111111 - 1)) + 11111111;

		}

        public static string Name(int length, bool withPrefix = true)
        {

	        var prefix  = withPrefix ? PREFIX : string.Empty;
	        var number  = Math.Abs(length);
	        var max		= number < 1 
							  ? 1
							  : number> 128
									? 128
									: number;

	        var sb		= new StringBuilder();
	        var random  = new Random();

	        for (var i = 0; i < max; i++)
	        {
		        var nb	= random.Next(0, LETTERS.Length);
		        var c	= LETTERS[nb];
		        sb.Append(c);
	        }

	        return $"{prefix}sb";
        }

        public static int RandomNumber(int length)
        {
	        var size = length < 1 || length > 8
						? 8
						: length;

	        var nb = GetRandomValues().ToString().Substring(0, size);
			return int.Parse(nb);
        }

		


	}
}
