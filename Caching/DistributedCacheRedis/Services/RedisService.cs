using StackExchange.Redis;

namespace DistributedCacheRedis.Services
{
    public class RedisService
    {
        private readonly string host;

        private readonly string port;

        private ConnectionMultiplexer redis;

        public IDatabase db;

        public RedisService(IConfiguration configuration)
        {
            host = configuration["Redis:host"];
            port = configuration["Redis:port"];

            var configString = $"{host}:{port}";
            redis = ConnectionMultiplexer.Connect(configString);

        }

        public IDatabase GetDatabase(int db)
        {
            return redis.GetDatabase(db);
        }
    }
}
