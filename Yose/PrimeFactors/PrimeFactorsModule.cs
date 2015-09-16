using System;
using Nancy;

namespace Yose
{
	public class PrimeFactorsModule : NancyModule
	{
		public PrimeFactorsModule (IPrimeFactors decomposer)
		{
			Get["/primeFactors"] = parameters => {
				int paramNumber = Int32.Parse(Request.Query.number);
				int[] result = decomposer.decomposition(paramNumber);
				return Response.AsJson (new { number = paramNumber, decomposition = result});
			};
		}
	}
}

