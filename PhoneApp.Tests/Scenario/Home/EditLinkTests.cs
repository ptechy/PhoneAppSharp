using NUnit.Framework;
using PhoneApp.Tests.Model;

namespace PhoneApp.Tests.Scenario.Home
{
	[TestFixture]
    public  class EditLinkTests : BaseTests
    {

	    [Test]
	    [Description("Navigate to homepage to verify honeBookApp link")]
	    [Expected("The PhoneBookApp link is present")]
	    public void CheckPhoneBookAppLink()
	    {
			HomePage.Header.ClickOnPhoneBookWebAppLink();
			TestCase.Assert(HomePage.Header.PhoneBookWebAppLinkElement.Text, "PhoneBookWebApp", "check phone book link is present");
	    }


	    [Test]
        [Description("Navigate to homepage to verify Home link")]
        [Expected("The Home link is present")]
        public void CheckHomeLink()
        {
	        HomePage.Header.ClickOnHomeLink();
	        TestCase.Assert(HomePage.Header.HomeLinkElement.Text,"Home", "check phone book link is present");
        }

    }
}