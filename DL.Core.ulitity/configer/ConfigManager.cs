using DL.Core.ulitity.tools;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DL.Core.ulitity.configer
{
    public  class ConfigManager
    {
      
        public IConfiguration  Configuration { get; set; }
        public ConfigManager()
        {
            var path = Directory.GetCurrentDirectory();
            Configuration = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile("appsettings.Development.json", optional: true)
             .Build();
        }
        public static ConfigManager Build
        {
            get
            {
                return new ConfigManager();
            }
        }
        public string GetDLSetting(string key)
        {
            return Configuration["DL:Setting" + key];
        }
        public string GetValue(string key)
        {
            return Configuration[key];
        }
        public Mail Mail => GetDLMailSetting();
        public DbConfig DbConfig => GetDLDbSetting();
        public SwaggerConfig SwaggerConfig => GetSwaggerSetting();
        public ConnectionString ConnectionString => GetConStrSetting();
        /// <summary>
        /// 获取DL节点的邮件配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private  Mail GetDLMailSetting()
        {
            var info = new Mail();
            var config = Configuration.GetSection("DL:Setting:Mail");
            info.SmtpPort = config["SmtpPort"].ToInt32();
            info.SmtpHost = config["SmHost"];
            info.SmtpPass = config["SmtpPass"];
            info.SendUser = config["SendUser"];
            return info;
        }

        /// <summary>
        /// 获取DL节点的DbConfig配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private  DbConfig GetDLDbSetting()
        {
            var config = Configuration.GetSection("DL:Setting:DbConfig");
            var db = new DbConfig();
            db.AutoAdoNetMiagraionEnable = Convert.ToBoolean(config["AutoAdoNetMiagraionEnable"]);
            db.AutoEFMigrationEnable = Convert.ToBoolean(config["AutoEFMigrationEnable"]);
            return db;
        }

        /// <summary>
        /// 获取DL节点的Swagger配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private  SwaggerConfig GetSwaggerSetting()
        {
            var swg = new SwaggerConfig();
            var config = Configuration.GetSection("DL:Setting:Swagger");
            swg.Enable = Convert.ToBoolean(config["Enable"]);
            swg.SwaggerName = config["SwaggerName"];
            swg.SwaggerDesc = config["SwaggerDesc"];
            swg.Version = config["Version"];
            swg.XmlAssmblyName = config["XmlAssmblyName"];
            swg.Authorization = Convert.ToBoolean(config["Authorization"]);
            swg.Issuer = config["Issuer"];
            swg.Audience = config["Audience"];
            swg.JwtSecret = config["JwtSecret"];
            return swg;
        }

        /// <summary>
        /// 获取数据库连接字符串配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private  ConnectionString GetConStrSetting()
        {
            var con = new ConnectionString();
            con.Default = Configuration["ConnectionString:default"];
            con.SqlDefault = Configuration["ConnectionString:SqlDefault"];
            con.MySqlDefault = Configuration["ConnectionString:MySqlDefault"];
            con.OracleDefault = Configuration["ConnectionString:OracleDefault"];
            return con;
        }
    }
}
