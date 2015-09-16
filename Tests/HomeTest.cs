using System;
using NUnit.Framework;
using Nancy.Testing;
using Yose;
using System.Web.Script.Serialization;

namespace Tests
{
	[TestFixture]
	public class HomeTest
	{
		private Browser browser;
		private BrowserResponse result;

		[SetUp]
		public void HomeModule()
		{
			browser = new Browser(with => with.Module(new HomeModule()));
			result = browser.Get("/", with => { with.HttpRequest(); });
		}

		[Test]
		public void HomePageContains_HelloYose ()
		{
			Assert.That(result.ContentType, Is.StringContaining("text/html"));
			Assert.That(result.Body.AsString, Is.StringContaining("Hello Yose"));
		}
	}
}


