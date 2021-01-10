using OpenQA.Selenium;

namespace PhoneApp.Core.Model.Search
{
	public class TableRow
	{
		public NameContainer NameContainer { get; }
		public IWebElement BinButton { get; }
		public PhoneNumberContainer PhoneNumberContainer { get; }
		public IWebElement EditLink { get; }
		public IWebElement EditImage { get; }
		public IWebElement DeleteButton { get; }

		public TableRow(NameContainer nameContainer, IWebElement binButton, PhoneNumberContainer phoneNumberContainer, 
			IWebElement editLink, IWebElement editImage, IWebElement deleteButton)
		{
			NameContainer = nameContainer;
			BinButton = binButton;
			PhoneNumberContainer = phoneNumberContainer;
			EditLink = editLink;
			EditImage = editImage;
			DeleteButton = deleteButton;
		}
	}
}
