using System;
using Nancy;

namespace Yose
{
	public class HomeModule : NancyModule
	{
		public HomeModule ()
		{
			Get["/"] = _ => Response.AsFile( "Content/index.html", "text/html");
		}
	}
}

