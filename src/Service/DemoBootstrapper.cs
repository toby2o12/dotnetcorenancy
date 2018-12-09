using Nancy;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcorenancy
{
	public class DemoBootstrapper : DefaultNancyBootstrapper
	{
		private readonly IAppConfiguration appConfig;

		public DemoBootstrapper()
		{
		}

		public DemoBootstrapper(IAppConfiguration appConfig)
		{
			this.appConfig = appConfig;
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			container.Register<IAppConfiguration>(appConfig);
		}
	}
}
