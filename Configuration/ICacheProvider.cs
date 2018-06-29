//from https://www.codeproject.com/Articles/1120038/A-simple-Csharp-cache-component-using-Redis-as-pro
using System;

namespace Common
{
    public interface ICacheProvider
    {
        void Set<T>(string key, T value);

        void Set<T>(string key, T value, TimeSpan timeout);

        T Get<T>(string key);

        bool Remove(string key);

        bool IsInCache(string key);
    }
}