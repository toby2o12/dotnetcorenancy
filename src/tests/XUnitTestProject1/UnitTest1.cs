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
			//1.1.ʵ�������ӹ���
			var factory = new ConnectionFactory() { HostName = "192.168.255.128", UserName = "guest",Password="guest"};

			//2. ��������
			using (var connection = factory.CreateConnection())
			{
				//3. �����ŵ�
				using (var channel = connection.CreateModel())
				{
					//4. ��������
					channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
					//5. ����byte��Ϣ���ݰ�
					string message = "Hello RabbitMQ!";
					var body = Encoding.UTF8.GetBytes(message);
					//6. �������ݰ�
					channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
					Console.WriteLine(" [x] Sent {0}", message);
				}
			}
		}
    }
}
