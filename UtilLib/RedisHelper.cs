using ServiceStack.Redis;
using System;

namespace EMU.Util
{
    public class RedisHelper
    {
        private RedisClient redis;
        public RedisHelper(string url)
        {
            string[] uri = url.Split(':');
            redis = new RedisClient(uri[0], int.Parse(uri[1]), "123456");
        }
        public void ChangeDB(long index)
        {
            redis.ChangeDb(index);
        }
        public void CloseRedis()
        {
            redis.Dispose();
            redis = null;
        }
        public void DeleteValue(string key)
        {
            redis.Del(key);
        }
        public T GetValue<T>(string key)
        {
            return redis.Get<T>(key);
        }
        public void SetValue<T>(string key, T value)
        {
            redis.Set<T>(key, value);
        }
    }
}
