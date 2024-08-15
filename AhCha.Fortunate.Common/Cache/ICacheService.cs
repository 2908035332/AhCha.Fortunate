
namespace AhCha.Fortunate.Common.Cache
{
    public interface ICacheService : IDisposable
    {
        string Get(string key);
        T Get<T>(string key);
        bool Set(string key, object value);
        bool Set(string key, object value, TimeSpan expire);
        List<String> GetAllKeys();

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        bool Exists(string key);
        long Del(params string[] key);
    }
}
