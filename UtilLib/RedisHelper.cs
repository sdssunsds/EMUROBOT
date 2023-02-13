using ServiceStack.Redis;
using System.Collections.Generic;

namespace EMU.Util
{
    public class RedisHelper
    {
        private bool isLink = false;
        private object redisLock = new object();
        private RedisClient redis;
        public RedisHelper(string url, string pwd)
        {
            string[] uri = url.Split(':');
            if (string.IsNullOrEmpty(pwd))
            {
                redis = new RedisClient(uri[0], int.Parse(uri[1]));
            }
            else
            {
                redis = new RedisClient(uri[0], int.Parse(uri[1]), pwd); 
            }
            isLink = true;
        }
        public void ChangeDB(long index)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    redis.ChangeDb(index);
                } 
            }
        }
        public void CloseRedis()
        {
            isLink = false;
            redis.Dispose();
            redis = null;
        }
        public void DeleteValue(string key)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    redis.Del(key);
                } 
            }
        }
        public List<string> GetKeys()
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    return redis.GetAllKeys();
                } 
            }
            return new List<string>();
        }
        public T GetValue<T>(string key)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    if (redis.ContainsKey(key))
                    {
                        return redis.Get<T>(key);
                    }
                }
            }
            return default(T);
        }
        public void SetValue<T>(string key, T value)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    redis.Set<T>(key, value);
                } 
            }
        }
    }
}
