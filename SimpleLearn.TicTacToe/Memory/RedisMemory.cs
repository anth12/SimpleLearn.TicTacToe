using Newtonsoft.Json;
using StackExchange.Redis;

namespace SimpleLearn.TicTacToe.Memory
{
    public class RedisMemory : IMemory
    {
        private static ConnectionMultiplexer redis;
        private readonly IDatabase db;
        private readonly string keyPrefix;

        public RedisMemory(string generationKey)
        {
            if(redis == null)
                redis = ConnectionMultiplexer.Connect("localhost");

            db = redis.GetDatabase();
            keyPrefix = generationKey + "-";
        }

        public Experience GetState(int state)
        {
            var payload = db.StringGet(keyPrefix + state);
            if(payload.HasValue)
                return JsonConvert.DeserializeObject<Experience>(payload);

            return new Experience();
        }

        public void SetState(int state, Experience data)
        {
            var payload = JsonConvert.SerializeObject(data);
            db.StringSet(keyPrefix + state, payload);
        }

    }
}
