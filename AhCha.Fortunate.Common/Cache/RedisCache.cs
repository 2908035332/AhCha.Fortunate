using CSRedis;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Cache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache
    {
        static RedisCache()
        {
            var csredis = new CSRedisClient(AhChaFortunateGlobalContext.RedisConnection);
            RedisHelper.Initialization(csredis);
        }

        public static String Get(string key)
        {
            return RedisHelper.Get(key);
        }

        public static T Get<T>(string key)
        {
            return RedisHelper.Get<T>(key);
        }

        public static Boolean Set(string key, object value)
        {
            return RedisHelper.Set(key, value);
        }

        public static Boolean Set(string key, object value, TimeSpan expire)
        {
            return RedisHelper.Set(key, value, expire);
        }

        public static List<String> GetAllKeys()
        {
            return RedisHelper.Keys("*").ToList();
        }

        public static Boolean Exists(string key)
        {
            return RedisHelper.Exists(key);
        }

        public static long Del(params string[] key)
        {
            return RedisHelper.Del(key);
        }

    }
}
