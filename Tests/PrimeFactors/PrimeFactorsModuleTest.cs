using System;
using NUnit.Framework;
using Nancy.Testing;
using Yose;
using System.Web.Script.Serialization;
using Moq;

namespace Tests
{
	[TestFixture]
	public class PrimeFactorsModuleTest
	{
		private Browser browser;
		private Mock<IPrimeFactors> decomposerMock;

		[SetUp]
		public void PrimeFactorsModule()
		{
			decomposerMock = new Mock<IPrimeFactors>();
			browser = new Browser(with => with.Module(new PrimeFactorsModule(decomposerMock.Object)));
		}

		[Test]
		public void NumberParameterIsPassedToDecomposer ()
		{
			DoRequest ("4", new int[] {2,2});
			decomposerMock.Verify(decomposer => decomposer.decomposition(4), Times.Exactly(1));
		}

		[Test]
		public void ReturnsAJson ()
		{
			var reponse = DoRequest ("2", new int[] {2});
			Assert.That(reponse.ContentType, Is.StringContaining("application/json"));
		}

		[Test]
		public void ReturnsTheValueExpectedByYose ()
		{
			var response = DoRequest ("16", new int[] {2,2,2,2});

			dynamic content = new JavaScriptSerializer ().Deserialize<dynamic>(response.Body.AsString());

			Assert.That(content["number"], Is.EqualTo(16));
			Assert.That(content["decomposition"], Is.EqualTo(new int[] {2,2,2,2}));
		}

		[Test]
		public void ReturnsNotANumberErrorInJSONWhenNumberParamIsAString ()
		{
			BrowserResponse response = browser.Get ("/primeFactors", with =>  {
				with.HttpRequest ();
				with.Query ("number", "AAA");
			});

			dynamic content = new JavaScriptSerializer ().Deserialize<dynamic>(response.Body.AsString());

			Assert.That(content["number"], Is.EqualTo("AAA"));
			Assert.That(content["error"], Is.EqualTo("not a number"));
		}

		private BrowserResponse DoRequest (string number , int[] result )
		{
			decomposerMock.Setup (decomposer => decomposer.decomposition (Int32.Parse(number))).Returns (result);
			BrowserResponse response = browser.Get ("/primeFactors", with =>  {
				with.HttpRequest ();
				with.Query ("number", number);
			});
			return response;
		}
	}
}
