using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMQPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName = "paymentQueue";

    public RabbitMQPublisher()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }
    public void PublishPaymentDetails(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
    }

    public void Close()
    {
        _connection.Close();
    }

}