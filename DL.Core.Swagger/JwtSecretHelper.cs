using DL.Core.ulitity.configer;
using DL.Core.ulitity.tools;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DL.Core.Swagger
{
    /// <summary>
    /// 产生JWT
    /// </summary>
    public static class JwtSecretHelper
    {
       // public static ConcurrentDictionary<string, ConcurrentDictionary<DateTime, string>> dic = new ConcurrentDictionary<string, ConcurrentDictionary<DateTime, string>>();
        /// <summary>
        /// 颁发JWT密钥
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string CreateSecret(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Email,"1352249378@qq.com"),
                new Claim(ClaimTypes.Sid,StrHelper.GetXGuid()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,StrHelper.GetDateGuid())
            };
            var config = ConfigManager.Build.SwaggerConfig;
            claims.Add(new Claim("username", userName));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSecret));
            var expiertime = DateTime.Now.AddMinutes(1);
            var token = new JwtSecurityToken(
                  issuer: config.Issuer,
                  claims: claims,
                  audience: string.IsNullOrWhiteSpace(config.Audience)?config.Issuer:config.Audience,
                  notBefore: DateTime.Now,
                  expires: expiertime,
                  signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            var values = new ConcurrentDictionary<DateTime, string>();
            values.TryAdd(expiertime, jwttoken);
            //dic.TryAdd("token", values);
            return jwttoken;
        }

    }
}
