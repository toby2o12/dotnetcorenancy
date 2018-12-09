using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using Xunit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using XUnitTestProject1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace XUnitTestProject1
{
	public class PomeloTest
	{
		public PomeloTest()
		{

		}
		[Fact]
		public async void test()
		{

			var sequence1 = new Sequence { };
			var sequence2 = new Sequence { };
			using (var scope = new AppDbScope())
			{
				scope.AppDb.Sequence.AddRange(new[] { sequence1, sequence2 });
				await scope.AppDb.SaveChangesAsync();
			}
			Assert.Equal(sequence1.Id + 1, sequence2.Id);
		}
		[Fact]
		public async void test2()
		{
			using (var scope = new AppDbScope())
			{
				var ret = await scope.AppDb.Set<Sequence>().FromSql("select * from Sequence").FirstOrDefaultAsync();
			}
		}
		[Fact]
		public void a()
		{
			using (var scope = new AppDbScope())
			{
				var _unitOfWork=scope._scope.ServiceProvider.GetService<IUnitOfWork>();
				
				var SequenceRepo = _unitOfWork.GetRepository<Sequence>();
				var userRepo = _unitOfWork.GetRepository<Users>();

				var ret =userRepo.Find(1).name;

				userRepo.Insert(new Users { age=20, name="jack" });
				userRepo.Insert(new Users { age = 30, name = "jack2" });

				SequenceRepo.Insert(new Sequence());
				_unitOfWork.SaveChanges();
			}
		}
		[Fact]
		public void AppDbAdd()
		{
			using (var scope = new AppDbScope())
			{
				scope.AppDb.Add(new Users { age = 20, name = "jack" });
				scope.AppDb.Add(new Users { age = 30, name = "jack2" });
				scope.AppDb.Add(new Sequence());
				scope.AppDb.SaveChanges();
			}
		}
	}
}
