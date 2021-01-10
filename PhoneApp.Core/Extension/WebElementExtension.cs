using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using PhoneApp.Core.Model.Search;

namespace PhoneApp.Core.Extension
{
	public static class WebElementExtension
	{
		public static TableRow ToTableRow(this IReadOnlyCollection<IWebElement> elements)
		{
			var tds = elements.ToList();
			var nameContainer = tds[0].ToNameContainer();
			var binButton = tds[0].GetBinButton();
			var phoneNumberContainer = tds[1].ToPhoneNumberContainer();
			var editLink = tds[2].GetEditLinkElement();
			var editImage = tds[2].GetEditImage();
			var deleteButton = tds[3].GetDeleteButton();
			return new TableRow(nameContainer,
				binButton,
				phoneNumberContainer,
				editLink,
				editImage,
				deleteButton);
		}

		public static IEnumerable<string> GetTableColumns(this IWebElement table)
		{
			return table.FindElements(By.TagName("th")).ToList()
				.Select(t => t.Text);
		}

		public static IEnumerable<TableRow> GetResultTable(this IWebElement table)
		{
			return table.FindElements(By.TagName("tr"))
				.Select(row => row.FindElements(By.TagName("td")))
				.ToList()
				.Where(r => r.Count == 4)
				.Select(ToTableRow);
		}

		public static NameContainer ToNameContainer(this IWebElement element)
		{
			var values = element.FindElement(By.TagName("span")).Text.Split().ToList();
			return new NameContainer(values);
		}

		public static IWebElement GetBinButton(this IWebElement element)
		{
			return element.FindElement(By.Id("deleteContactBtn"));
		}

		public static PhoneNumberContainer ToPhoneNumberContainer(this IWebElement element)
		{
			return new PhoneNumberContainer(element.Text);
		}

		public static IWebElement GetEditLinkElement(this IWebElement element)
		{
			return element.FindElement(By.TagName("a"));
		}

		public static IWebElement GetEditImage(this IWebElement element)
		{
			return element.FindElement(By.Id("editContactContainer"));
		}


		public static IWebElement GetDeleteButton(this IWebElement element)
		{
			return element.FindElement(By.Id("deleteContactPhone"));
		}
	}
}
