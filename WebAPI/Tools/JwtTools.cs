using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace WebAPI.Tools
{
    public class JwtTools
    {

       public static  string key = System.Web.Configuration.WebConfigurationManager.AppSettings["name"];
       /// <summary>
       /// Jwt加密
       /// </summary>
       /// <param name="payload">明文部分</param>
       /// <param name="key">秘钥</param>
       /// <returns>返回Token</returns>
        public static string Encode(Dictionary<string,object>payload,string key)
        {

            IJwtAlgorithm algorithm = new JWT.Algorithms.HMACSHA256Algorithm();//hash256 加密算法
            IJsonSerializer serializer = new JsonNetSerializer();//序列化 加密器
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//转译
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, key);//开始加密
        }

        internal static object ValideLogined(HttpRequestHeaders headers)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Jwt解密
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="key"秘钥</param>
        /// <returns>返回明文部分</returns>
        public static string Decode(string token, string key)
        {
            try
            {
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, key, verify: true);
                return json;
            }
            catch (TokenExpiredException ex) 
            {
                Console.WriteLine("Token has expired");
                throw;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
                throw;
            }
        }


        public static string ValideLogined(HttpRequest request) {
            if (string.IsNullOrWhiteSpace(request.Headers["token"]))
                throw new Exception("请重新登录");
            return Decode(request.Headers["token"], key);
            

        }
    }
}