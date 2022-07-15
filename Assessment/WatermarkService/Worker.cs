using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Drawing;
using System.Text;
using System.Text.Json;
using WatermarkService.Services;

namespace WatermarkService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;

        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private Task Consumer_Received(object sender , BasicDeliverEventArgs @event)
        {
            try
            {
                var imageCreatedEvent = JsonSerializer.Deserialize<CustomerImageCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));

                var basePath = Directory.GetCurrentDirectory().Replace("WatermarkService", "Assessment.API");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageCreatedEvent.ImageName).Replace("WatermarkService", "Assessment.API");

                using var img = Image.FromFile(path);
                using var graphics = Graphics.FromImage(img);

                var text = "www.mysite.com/fd";

                var font = new Font(FontFamily.GenericMonospace, 32, FontStyle.Italic, GraphicsUnit.Pixel);
                var textSize = graphics.MeasureString(text, font);
                var color = Color.White;
                var brush = new SolidBrush(color);
                var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

                graphics.DrawString(text, font, brush, position);

                img.Save($"{basePath}/wwwroot/images/watermarks/" + imageCreatedEvent.ImageName);
                img.Dispose();
                graphics.Dispose();

                _logger.LogInformation("Watermark added succesfully for " + imageCreatedEvent.ImageName);

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Task.CompletedTask;

        }

    }
}