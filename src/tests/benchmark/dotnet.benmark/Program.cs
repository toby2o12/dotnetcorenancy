using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using dotnet.benmark.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace dotnet.benmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchMarkMysql>();
            System.Console.ReadLine();
        }
    }
    // [ClrJob(baseline: true), CoreJob, MonoJob, CoreRtJob]
    // [RPlotExporter, RankColumn]
    // [MinColumn, MaxColumn, MedianColum]
    [MemoryDiagnoser]
    // [RankColumn(NumeralSystem.Arabic)]
    // [RankColumn(NumeralSystem.Roman)]
    // [RankColumn(NumeralSystem.Stars)]
    public class BenchMarkMysql
    {
        [Benchmark(Baseline = true)]
        public void EfInsertTest()
        {
            using (var scope = new AppDbScope())
            {
                var _unitOfWork = scope._scope.ServiceProvider.GetService<IUnitOfWork>();
                var userRepo = _unitOfWork.GetRepository<Users>();
                int age = new Random(10).Next(1, 100);
                userRepo.Insert(new Users { age = age, name = "jack" + age });
                _unitOfWork.SaveChanges();
            }
        }
        [Benchmark]
        public void DapperTest()
        {
            using (var scope = new AppDbScope())
            {
                scope.AppDb.Users.Add(new Users { age = 10, name = "jack" });
                scope.AppDb.SaveChanges();
            }
        }
    }
}
