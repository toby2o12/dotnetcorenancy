using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
namespace dotnetcorenancy
{
    class Program
    {
        static void Main(string[] args)
        {
			test1();
			Console.WriteLine("start");
			var host = new WebHostBuilder()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseKestrel()
				.UseUrls("http://*:5000")
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
		static async void test1()
		{
			string path = System.IO.Path.Combine(@"D:\git-maryun\corefx","build.cmd");
			var ret=await File.ReadAllLinesAsync(path);
			System.Threading.Thread.Sleep(3000);
			Console.WriteLine(ret);
		}
    }
}
