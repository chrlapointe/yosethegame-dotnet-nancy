using System;
using NUnit.Framework;
using Nancy.Testing;
using Yose;
using System.Web.Script.Serialization;
using Nancy;
using CsQuery;

namespace Tests
{
	[TestFixture]
	public class HomeTest
	{
		private Browser browser;
		private BrowserResponse result;

		[SetUp]
		public void HomeModule ()
		{
			browser = new Browser (with => with.Module (new HomeModule ()));
			result = browser.Get ("/", with => {
				with.HttpRequest ();
			});
		}

		[Test]
		public void HomePageContains_HelloYose ()
		{
			Assert.That (result.ContentType, Is.StringContaining ("text/html"));
			Assert.That (result.Body.AsString (), Is.StringContaining ("Hello Yose"));
		}

		[Test]
		public void ContactLinkIsAvailable_WithIdContachLink_PortefolioContactInformation ()
		{
			result.Body ["a#contact-me-link"]
				.ShouldExistOnce ();
		}

		[Test]
		public void StartWorld_ShareChallenge_ALinkExistsToTheSourceRepo ()
		{
			result.Body ["a#repository-link"]
				.ShouldExistOnce ();
		}

		[Test]
		public void StartWorld_ShareChallenge_TheRepoUrlIsValid ()
		{
			var enumerator = result.Body ["a#repository-link"].GetEnumerator ();
			enumerator.MoveNext(); 
			NodeWrapper node = enumerator.Current;
			CQ dom = CQ.CreateFromUrl(node.Attributes ["href"]);

			var readmeDomElement = dom.Document.GetElementById ("readme");
			Assert.That (readmeDomElement, Is.Not.Null);
			Assert.That (readmeDomElement.InnerHTML, Is.StringContaining("YoseTheGame"));
		}
	}
}


