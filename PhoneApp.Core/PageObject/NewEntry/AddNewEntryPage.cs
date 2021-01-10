using System.Collections.Generic;
using OpenQA.Selenium;
using PhoneApp.Core.Context;
using PhoneApp.Core.Extension;
using PhoneApp.Core.Helper;
using PhoneApp.Core.Model;

namespace PhoneApp.Core.PageObject.NewEntry
{
	public class AddNewEntryPage :BasePage
	{

		public  static readonly Dictionary<EntryEnum, string>  LocatorDictionary = new Dictionary<EntryEnum, string>
		{
			{EntryEnum.FirstName,   "firstName"},
			{EntryEnum.LastName,   "lastName"},
			{EntryEnum.CountryCode,  "countryCode"},
			{EntryEnum.AreaCode,	"areaCode"},
			{EntryEnum.PhoneNumber, "phoneNumber"}
		};

		private readonly Dictionary<EntryEnum,IWebElement> formControls;

		private static string SubmitButtonLocator = "//button[contains(.,'Submit')]";
		private static string ClearAllButtonLocator = "//button[contains(.,'Clear all')]";

		private static string ConfirmButton = "//button[contains(.,'Confirm')]";
		private static string CancelButton = "//button[contains(.,'Cancel')]";

		private static string SuccessSaveMsg = "//div[@class='alert alert-success fade show']";
		private static string FailureSaveMsg = "//div[@class='alert alert-danger fade show']";
		

		public IWebElement SubmitButton { get; }
		private IWebElement ClearAllButton { get; set; }

		public AddNewEntryPage(Browser browser) : base(browser)
		{
			formControls		= new Dictionary<EntryEnum, IWebElement>();
			browser.StepLogger.AddStep("Wait for AddNewEntryPage to be loaded");
			SubmitButton		= Browser.Driver.WaitForElementVisible(By.XPath(SubmitButtonLocator));

			CreateEntryForm();
		}

		private void CreateEntryForm()
		{
			foreach (var control in LocatorDictionary)
			{
				Browser.StepLogger.AddStep($"Wait for Id {control.Key} to be visible");
				formControls.Add(control.Key, Browser.Driver.WaitForElementVisible(By.Id(control.Value)));
			}
		}

		public AddNewEntryPage FillForm(Contact contact)
		{
			var formValues = contact.GetFormValues();
			foreach (var data in formValues)
			{
				FillForm(data.Key, data.Value);
			}

			return this;
		}

		

		public AddNewEntryPage FillForm(EntryEnum entryEnum, string value )
		{
			Browser.StepLogger.AddStep($"Fill: {entryEnum} Form with value: {value}");
			var control = formControls[entryEnum];
			control.Clear();
			control.SendKeys(value);
			return this;
		}

		public AddNewEntryPage ClickOnClearAll()
		{
			Browser.StepLogger.AddStep("Click on Clear all button");
			ClearAllButton = Browser.Driver.WaitForElementVisible(By.XPath(ClearAllButtonLocator));
			ClearAllButton.Click();
			return this;
		}

		public AddNewEntryPage ClickOnSubmit()
		{
			Browser.StepLogger.AddStep("Click on submit button");
			SubmitButton.Click();
			return this;
		}

		public string GetSuccessMsgAfterSubmit()
		{
			Browser.StepLogger.AddStep($"Wait for success message to be visible: {SuccessSaveMsg}");
			return  Browser.Driver.WaitForElementVisible(By.XPath(SuccessSaveMsg), 20).Text;
		}

		public string GetFailureMsgAfterSubmit()
		{
			Browser.StepLogger.AddStep($"Wait for error message to be visible: {FailureSaveMsg}");
			return Browser.Driver.WaitForElementVisible(By.XPath(FailureSaveMsg)).Text;
		}


		public AddNewEntryPage ClickOnConfirm()
		{
			Browser.StepLogger.AddStep("Click on confirm button");
			Browser.Driver.WaitForElementVisible(By.XPath(ConfirmButton)).Click();
			return this;
		}

		public AddNewEntryPage ClickOnCancel()
		{
			Browser.StepLogger.AddStep("Click on cancel");
			Browser.Driver.WaitForElementVisible(By.XPath(CancelButton)).Click();
			return this;
		}
	}
}
