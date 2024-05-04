using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

public class PaymentQueueConsumer
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "paymentQueue";
    private IConnection _connection;
    private IModel _channel;

    public PaymentQueueConsumer()
    {
        var factory = new ConnectionFactory() { HostName = _hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void StartConsuming()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var paymentDetails = JsonSerializer.Deserialize<dynamic>(message);

            Console.WriteLine($"Received payment confirmation: {paymentDetails}");
        };

        _channel.BasicConsume(queue: _queueName,
                             autoAck: true,
                             consumer: consumer);
    }

    public void StopConsuming()
    {
        _connection.Close();
    }
}
