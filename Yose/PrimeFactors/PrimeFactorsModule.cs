using System;
using Nancy;

namespace Yose
{
	public class PrimeFactorsModule : NancyModule
	{
		public PrimeFactorsModule (IPrimeFactors decomposer)
		{
			Get["/primeFactors"] = parameters => {
				try {
					int paramNumber = Int32.Parse(Request.Query.number);
					int[] result = decomposer.decomposition(paramNumber);
					return Response.AsJson (new { number = paramNumber, decomposition = result});
				} catch (Exception ex) {
					return Response.AsJson(new  { number = Request.Query.number, error = "not a number"});
				}
			};
		}
	}
}

