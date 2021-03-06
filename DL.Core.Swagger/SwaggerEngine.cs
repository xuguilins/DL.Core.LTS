﻿using DL.Core.ulitity.configer;
using DL.Core.ulitity.log;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DL.Core.Swagger
{
    public static class SwaggerEngine
    {
        private static string version = string.Empty;
        private static string title = string.Empty;
        private static bool engalbe = false;
        private static string docName = string.Empty;
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// DLCore针对Swagger引用的扩展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerPack(this IServiceCollection services)
        {
            try
            {
                var swaggerInfo = ConfigManager.Build.SwaggerConfig;
                if (swaggerInfo != null)
                {
                    if (swaggerInfo != null)
                    {
                        var swg = swaggerInfo;
                        if (swg.Enable)
                        {
                            engalbe = swg.Enable;
                            title = swg.Title;
                            version = swg.Version;
                            docName = swg.SwaggerName;
                            services.AddSwaggerGen(options =>
                            {
                                options.SwaggerDoc(docName, new Microsoft.OpenApi.Models.OpenApiInfo
                                {
                                    Description = swg.SwaggerDesc,
                                    Title = title,
                                    Version = version
                                });
                                if (swg.Authorization)
                                {
                                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                                    {
                                        Description = "请求头中需要添加Jwt授权Token：Bearer Token",
                                        Name = "Authorization",
                                        In = ParameterLocation.Header,
                                        Type = SecuritySchemeType.ApiKey
                                    });
                                    options.OperationFilter<AddResponseHeadersFilter>();
                                    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                                }
                                if (!string.IsNullOrWhiteSpace(swg.XmlAssmblyName))
                                {
                                  
                                    var xmlList = swg.XmlAssmblyName.Split(',');
                                    foreach (var xml in xmlList)
                                    {
                                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
                                        options.IncludeXmlComments(xmlPath);
                                    }
                                } else
                                {
                                    var path = AppContext.BaseDirectory;
                                    var files = Directory.GetFiles(path, "*.xml",SearchOption.TopDirectoryOnly);
                                    foreach (var item in files)
                                    {
                                        options.IncludeXmlComments(item);
                                    }
                                }                      
                            });
                            if (swg.Authorization)
                            {
                                #region [添加JWT认证]
                                // 添加验证服务
                                services.AddAuthentication(x =>
                                {
                                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                }).AddJwtBearer(o =>
                                {
                                    o.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        // 是否开启签名认证
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(swg.JwtSecret)), //密钥
                                        // 发行人验证，这里要和token类中Claim类型的发行人保持一致
                                        ValidateIssuer = true,
                                        ValidIssuer = swg.Issuer,//发行人
                                        ValidateAudience = false,
                                        ValidAudience = (string.IsNullOrWhiteSpace(swg.Audience)?swg.Issuer:swg.Audience),//接收人
                                        ValidateLifetime = true,
                                        ClockSkew = TimeSpan.Zero,
                                    };
                                });
                                #endregion 

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"创建swagger文件发生异常:${ex.Message}");
            }
            return services;
        }

        /// <summary>
        ///  DLCore针对Swagger引用的扩展
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerPack(this IApplicationBuilder app)
        {
            try
            {
                if (engalbe)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(option =>
                    {
                        option.SwaggerEndpoint($"/swagger/{docName}/swagger.json", docName);
                    });
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Use Swagger发生异常，ex:{ex.Message}");
            }
        }
    }
}
