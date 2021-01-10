using System;
using NUnit.Framework;

namespace PhoneApp.Tests.Model
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public class Expected : PropertyAttribute
	{
		public Expected(string propertyValue) : base(propertyValue)
		{
		}
	}
}
