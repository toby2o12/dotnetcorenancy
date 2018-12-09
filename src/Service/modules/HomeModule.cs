using Nancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcorenancy.modules
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get("/", args => "Hello World, it's Nancy on .NET Core");
		}
	}
}
