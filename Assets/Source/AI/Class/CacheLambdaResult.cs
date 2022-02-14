using System;
using System.Collections.Generic;

public static class CacheLambdaResult
{
    public static class Cache
    {
        private static Dictionary<string, object> _cache = new Dictionary<string, object>();

        public static void Store(string key, object value)
        {
            _cache[key] = value;
        }

        public static TR Retrieve<TR>(string key)
        {
            if (_cache.ContainsKey(key))
            {
                return (TR)_cache[key];
            }
    
            return default;
        }
    
        public static void Clear()
        {
            _cache.Clear();
        }
    }
    
    public static TR CacheResult<TR>(Func<TR> lambda)
    {
        var methodName = lambda.Method.Name;

        var fieldInfos = lambda.Target.GetType().GetFields();

        var stringKey = methodName;

        foreach (var fieldInfo in fieldInfos)
        {
            stringKey += fieldInfo.GetValue(lambda.Target).ToString();
        }

        var result = Cache.Retrieve<TR>(stringKey);
        
        if (result == null)
        {
            result = (TR)lambda.DynamicInvoke();
            Cache.Store(stringKey, result);
        }

        return result;
    }
}