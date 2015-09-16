using System;
using NUnit.Framework;
using Yose;

namespace Tests
{
	[TestFixture]
	public class PrimeFactorsTest
	{
		PrimeFactors primeFactors = new PrimeFactors ();

		[Test]
		public void ReturnsTwoForTwo ()
		{
			Assert.That(primeFactors.decomposition (2), Is.EqualTo(new int[]{2}));
		}

		[Test]
		public void ReturnsPowerOfTwo ()
		{
			Assert.That(primeFactors.decomposition (16), Is.EqualTo(new int[]{2,2,2,2}));
		}
	}
}
