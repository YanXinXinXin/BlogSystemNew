using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace WebAPI.Tools
{
    public class JwtTools
    {

       private static  readonly string KEY = System.Web.Configuration.WebConfigurationManager.AppSettings["name"];
       /// <summary>
       /// Jwt加密
       /// </summary>
       /// <param name="payload">明文部分</param>
       /// <param name="key">秘钥</param>
       /// <returns>返回Token</returns>
        public static string Encode(Dictionary<string,object>payload)
        {

            IJwtAlgorithm algorithm = new JWT.Algorithms.HMACSHA256Algorithm();//hash256 加密算法
            IJsonSerializer serializer = new JsonNetSerializer();//序列化 加密器
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//转译
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, KEY);//开始加密
        }
       
        

        /// <summary>
        /// Jwt解密
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="key"秘钥</param>
        /// <returns>返回明文部分</returns>
        public static Dictionary<string, object> Decode(string token)
        {
            try
            {

                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var jsonStr = decoder.Decode(token, KEY, verify: true);
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
                if ((DateTime)result["exp"] < DateTime.Now)
                    throw new Exception("JWT:Token已过期,请重新登录");
                result.Remove("exp");
                return result;
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

        /// <summary>
        /// 校验是否登录过
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        internal static object ValideLogined(HttpRequestHeaders headers)
        {
            if (headers.GetValues("token") == null || !headers.GetValues("token").Any())
            {
                throw new Exception("请登陆");
            }
            return Decode(headers.GetValues("token").First());
        }
    }
}