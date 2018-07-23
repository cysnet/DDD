using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Infrastructure.Redis;
using CSRedis;

namespace Bbing.Infrastructure
{
    public class CSRedisHelper: IRedisHelper
    {
        private static string environment;
        private static string redisHost;
        private static string redisPassword;
        private static int redisPort;

        static CSRedisHelper()
        {
            environment = "Environment".GetAppSetting();
            redisHost = $"RedisHost.{environment}".GetAppSetting();
            redisPassword = $"RedisPassword.{environment}".GetAppSetting();
            redisPort = Convert.ToInt32($"RedisPort.{environment}".GetAppSetting());
        }

        public void Set(string key, string value)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                redis.Set(key, value);
            }
        }

        public void Set(string key, string value,TimeSpan span)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                redis.Set(key, value, span);
            }
        }

        public void Set<T>(string key, T value)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                redis.Set(key, value.ObjToString());
            }
        }

        public void Set<T>(string key, T value, TimeSpan span)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                redis.Set(key, value.ObjToString(), span);
            }
        }

        public bool Exist(string key)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                return redis.Exists(key);
            }
        }

        public string Get(string key)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                return redis.Get(key);
            }
        }

        public T Get<T>(string key)
        {
            using (var redis = new RedisClient(redisHost, redisPort))
            {
                redis.Connected += (s, e) => redis.Auth(redisPassword); // set AUTH, CLIENT NAME, etc
                return redis.Get(key).StringToObj<T>();
            }
        }
    }
}
