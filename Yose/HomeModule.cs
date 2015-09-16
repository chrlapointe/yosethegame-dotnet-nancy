using System;
using Nancy;

namespace Yose
{
	public class HomeModule : NancyModule
	{
		public HomeModule ()
		{
			Get["/"] = _ => "Hello Yose!";
		}
	}
}

