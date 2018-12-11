using BenchmarkDotNet.Running;

namespace XUnitTestProject1
{
    public class Pragram
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchMarkMysql>();
            System.Console.ReadLine();
        }
    }
}