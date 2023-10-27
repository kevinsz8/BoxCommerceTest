namespace ManufacturerVehicles.Communication.Services
{
    public interface IRabbitMQInterface
    {
        public Task SendProductMessage<T>(T message);
    }
}