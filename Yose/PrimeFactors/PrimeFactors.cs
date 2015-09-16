using System;
using System.Collections.Generic;

namespace Yose
{
	public class PrimeFactors : IPrimeFactors
	{
		#region IPrimeFactors implementation

		public int[] decomposition (int number)
		{
			List<int> result = new List<int>();
			while (number > 1) {
				result.Add (2);
				number /= 2;
			}
			return result.ToArray();
		}

		#endregion
	}

}

