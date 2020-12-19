using System;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.log;
using DL.Core.ulitity.EventBusHandler;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using DL.Core.Mediator;
using System.IO;
using System.Text;
using System.Collections.Generic;
using DL.Core.ulitity.tools;
using System.Linq;
using DL.Core.EfCore.engine;
using DL.Core.EfCore.packBase;
using DL.Core.ulitity.attubites;
using System.ComponentModel;
using System.Threading.Tasks;
using DL.Core.ulitity.ui;
using DL.Core.ulitity.CommandBuilder;
using System.Data;
using Quartz.Core;
using Quartz;
using Quartz.Util;
using Quartz.Impl;
using DL.Core.Ado.MySql;
using DL.Core.Ado;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ConsoleApp2
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            IMySqlDbContext service = new MySqlDbContext();
            List<testinfo> list = new List<testinfo>();
            service.CreateDbConnection("Data Source=localhost;port=3306; database=test_business;User=root;Password=123456");
            for (int i = 301; i < 400; i++)
            {
                var sql = "INSERT INTO testinfo(ID,UserName,Password,CreateTime,Price,OutPrice,InPrice)VALUES(@ID,@UserName,@Password,@CreateTime,@Price,@OutPrice,@InPrice)";
                DbParameter[] ps =
                {
                     new MySqlParameter("@ID",i),
                     new MySqlParameter("@UserName",$"user{i}"),
                     new MySqlParameter("@Password",$"ppass{i}"),
                     new MySqlParameter("@CreateTime",DateTime.Now),
                     new MySqlParameter("@Price",12),
                     new MySqlParameter("@OutPrice",12),
                     new MySqlParameter("@InPrice",12),

                };
                service.ExecuteNonQuery(sql, CommandType.Text, ps);

            }

            Console.ReadKey();
        }
    }
    public class testinfo
    {
        /// <summary>
        ///                      
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public double InPrice { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public double OutPrice { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public string UserName { get; set; }
    }


}

