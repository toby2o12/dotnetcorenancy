using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace dotnet.benmark
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = AppConfig.Config;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddMvc(options =>
            //{
            //	options.OutputFormatters.Clear();
            //	options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
            //	{
            //		ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //	}, ArrayPool<char>.Shared));
            //});
            ConfigureEntityFramework(services);

            services.AddIdentity<Model.AppIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDb>()
                .AddDefaultTokenProviders();
            services.AddUnitOfWork<AppDb>();
        }

        public static void ConfigureEntityFramework(IServiceCollection services, DbConnection connection = null)
        {
            if (connection == null)
            {
                services.AddDbContextPool<AppDb>(
                    options => options.UseMySql(AppConfig.Config["Data:ConnectionString"],
                        mysqlOptions =>
                        {
                            mysqlOptions.MaxBatchSize(AppConfig.EfBatchSize);
                            mysqlOptions.ServerVersion(AppConfig.Config["Data:ServerVersion"]);
                            if (AppConfig.EfRetryOnFailure > 0)
                                mysqlOptions.EnableRetryOnFailure(AppConfig.EfRetryOnFailure, TimeSpan.FromSeconds(5), null);
                        }
                ));
            }
            else
            {
                services.AddDbContext<AppDb>(
                    options => options.UseMySql(connection,
                        mysqlOptions =>
                        {
                            mysqlOptions.MaxBatchSize(AppConfig.EfBatchSize);
                            mysqlOptions.ServerVersion(AppConfig.Config["Data:ServerVersion"]);
                            if (AppConfig.EfRetryOnFailure > 0)
                                mysqlOptions.EnableRetryOnFailure(AppConfig.EfRetryOnFailure, TimeSpan.FromSeconds(5), null);
                        }
                ));
            }
            services.AddUnitOfWork<AppDb>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseMvc();
        }
    }
}
