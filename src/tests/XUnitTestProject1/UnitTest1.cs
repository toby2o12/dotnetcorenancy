using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
			//1.1.实例化连接工厂
			var factory = new ConnectionFactory() { HostName = "192.168.255.128", UserName = "guest",Password="guest"};

			//2. 建立连接
			using (var connection = factory.CreateConnection())
			{
				//3. 创建信道
				using (var channel = connection.CreateModel())
				{
					//4. 申明队列
					channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
					//5. 构建byte消息数据包
					string message = "Hello RabbitMQ!";
					var body = Encoding.UTF8.GetBytes(message);
					//6. 发送数据包
					channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
					Console.WriteLine(" [x] Sent {0}", message);
				}
			}
		}
    }
}
