using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Nancy.Owin;

namespace dotnetcorenancy
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseOwin(x => x.UseNancy());
		}
	}
}
