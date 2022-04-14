using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT
{
    class Program
    {
        static void Main(string[] args)
        {
            //#region 加密
            //var payload = new Dictionary<string, object>
            //{
            //    {"UserId",123 },
            //     {"UserName","admin" }
            //};
            //var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk"; //秘钥

            //IJwtAlgorithm algorithm = new Algorithms.HMACSHA256Algorithm();//hash256 加密算法
            //IJsonSerializer serializer = new JsonNetSerializer();//序列化 加密器
            //IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//转译
            //IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            //var token = encoder.Encode(payload, secret);//开始加密
            //Console.WriteLine(token);
            //#endregion



            #region 解密
            var tokenStr = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJVc2VySWQiOjEyMywiVXNlck5hbWUiOiJhZG1pbjIifQ.NJBW05GZ5B0DCwL1sVkaPX4VxfxuLo4GFP8YtuErpJA";
            var secret2 = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            try
            {
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer,validator,urlEncoder, algorithm);

                var json = decoder.Decode(tokenStr, secret2, verify: true);
                Console.WriteLine(json);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
            #endregion
        }
    }
}
