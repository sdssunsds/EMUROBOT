using StackExchange.Redis;
using System.Collections.Generic;

namespace EMU.Util
{
    public class RedisHelper
    {
        private bool isLink = false;
        private object redisLock = new object();
        private ConnectionMultiplexer redis;
        private IDatabase database;
        public RedisHelper(string url, string pwd)
        {
            redis = ConnectionMultiplexer.Connect(url + ",password=" + pwd);
            database = redis.GetDatabase();
            isLink = true;
        }
        public void ChangeDB(long index)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    redis.GetDatabase((int)index);
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
                    database.KeyDelete(key);
                } 
            }
        }
        public List<string> GetKeys(string key)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    var server = redis.GetServer(redis.GetEndPoints()[0]);
                    var keys = server.Keys(0, key + "*");
                    if (keys != null)
                    {
                        List<string> vs = new List<string>();
                        foreach (var item in keys)
                        {
                            vs.Add((string)item);
                        }
                        return vs;
                    }
                } 
            }
            return new List<string>();
        }
        public string Get(string key)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    return database.StringGet(key);
                }
            }
            return "";
        }
        public void Set(string key, string value)
        {
            if (isLink)
            {
                lock (redisLock)
                {
                    database.StringSet(key, value);
                }
            }
        }
    }
}
