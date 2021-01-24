
using DL.Core.EfCore;
using DL.Core.EfCore.engine;
using DL.Core.EfCore.SqlServer;
using DL.Core.ulitity.ui;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using DL.Core.ulitity.attubites;
using DL.Core.EfCore.MySql;
using System.Collections.Generic;
using DL.Core.Ado.SqlServer;
using DL.Core.Ado.Oracle;
using System.Data;
using DL.Core.ulitity.tools;
using System.IO;
using DL.Core.ulitity.configer;
using DL.Core.ulitity.table;
namespace ConsoleApp2
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            ISqlServerDbContext context = new SqlServerDbContext();
            context.CreateDbConnection("Data Source=.;Initial Catalog=CodeFormDB;Persist Security Info=True;User ID=sa;Password=0103");
            var sql = string.Format("SELECT * FROM USERINFO");
            var list = context.GetDataTable(sql, CommandType.Text).ToObjectList<UserInfo>();


            Console.ReadKey();
        }
    }
    public class UserInfo
    {
        /// <summary>
        ///                      
        /// </summary>
        public DateTime CREATETIME { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public int USERAGE { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        ///                      
        /// </summary>
        public string USERPASS { get; set; }
    }

}
