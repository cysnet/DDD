using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Builder;

namespace Bbing.Infrastructure
{
    public class JwtHelper
    {
        private static string secret;
        static JwtHelper()
        {
            string environment = "Environment".GetAppSetting();
            secret = $"JwtSecret.{environment}".GetAppSetting();
        }

        /// <summary>
        /// 获取jwtToken
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetJwtToken(Dictionary<string,string>data)
        {
            var tokenBuilder = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(secret);
            foreach (var keyValuePair in data)
            {
                tokenBuilder.AddClaim(keyValuePair.Key, keyValuePair.Value);
            }
            return tokenBuilder.Build();
        }

        /// <summary>
        /// 验证jwtToken
        /// </summary>
        /// <param name="token"></param>
        public static Dictionary<string,string> ValidateJwtToken(string token)
        {
            try
            {
                var json = new JwtBuilder()
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode(token);
                return json.StringToObj<Dictionary<string, string>>();
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
                return null;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
