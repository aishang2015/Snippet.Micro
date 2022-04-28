using StackExchange.Redis;

namespace Snippet.Micro.Redis
{
    public class RedisClient
    {
        private readonly RedisOption _redisOption;

        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisClient(RedisOption redisOption)
        {
            _redisOption = redisOption;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisOption.ConnectionString);
        }

        public IDatabase GetDatabase(int db = 0)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }


        public IDatabase DB01() => _connectionMultiplexer.GetDatabase(1);
        public IDatabase DB02() => _connectionMultiplexer.GetDatabase(2);
        public IDatabase DB03() => _connectionMultiplexer.GetDatabase(3);
        public IDatabase DB04() => _connectionMultiplexer.GetDatabase(4);
        public IDatabase DB05() => _connectionMultiplexer.GetDatabase(5);
        public IDatabase DB06() => _connectionMultiplexer.GetDatabase(6);
        public IDatabase DB07() => _connectionMultiplexer.GetDatabase(7);
        public IDatabase DB08() => _connectionMultiplexer.GetDatabase(8);
        public IDatabase DB09() => _connectionMultiplexer.GetDatabase(9);
        public IDatabase DB10() => _connectionMultiplexer.GetDatabase(10);
        public IDatabase DB11() => _connectionMultiplexer.GetDatabase(11);
        public IDatabase DB12() => _connectionMultiplexer.GetDatabase(12);
        public IDatabase DB13() => _connectionMultiplexer.GetDatabase(13);
        public IDatabase DB14() => _connectionMultiplexer.GetDatabase(14);
        public IDatabase DB15() => _connectionMultiplexer.GetDatabase(15);

    }
}
