using System;
using System.Collections.Generic;
using System.Linq;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity.ui;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.finder;
using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DL.Core.ns.EFCore;
using DL.Core.EfCore.engine;
using System.Runtime.CompilerServices;
using DL.Core.ulitity.attubites;
using DL.Core.ulitity.configer;
using DL.Core.ulitity.log;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ConsoleApp2
{
    class Program
    {
        //  static List<RootBpmUser> list = new List<RootBpmUser>();
        static void Main(string[] args)
        {
            ISqlServerDbContext context = new SqlServerDbContext();
            context.CreateDbConnection("Data Source=10.10.12.25;Initial Catalog=BPMDB;User ID=sa;Password=bpm123#");
            int count = 0;
            var table = context.GetPageDataTable("BPMSysUsers", 1, 5, "Account", out count, "AND DisplayName LIKE '%王%'");

            Console.ReadKey();
        }
        public static List<RootBpmUser> GetRecoveBpmUser(ISqlServerDbContext context, string parentId = null)
        {
            List<RootBpmUser> list = new List<RootBpmUser>();
            string sql = string.Empty;
            DataTable dt = null;
            if (parentId == null)
            {
                sql = "select * from BPMSysOUs where ParentOUID IS NULL ";
                dt = context.GetDataTable(sql, CommandType.Text);

            }
            else if (parentId == "3") {

                sql = "SELECT * FROM BPMSysOUs WHERE ParentOUID='" + parentId + "'  and OUName like '%系'";
                dt = context.GetDataTable(sql, CommandType.Text);
            } else
            {
                sql = "SELECT * FROM BPMSysOUs WHERE ParentOUID='" + parentId + "'";
                dt = context.GetDataTable(sql, CommandType.Text);
            }
            foreach (DataRow row in dt.Rows)
            {
                RootBpmUser bpm = new RootBpmUser();
                if (row["OUID"] != DBNull.Value)
                    bpm.OUID = row["OUID"].ToString();
                if (row["OUNAME"] != DBNull.Value)
                    bpm.OUNAME = row["OUNAME"].ToString();
                if (row["ParentOUID"] != DBNull.Value)
                    bpm.ParentId = row["ParentOUID"].ToString();
                if (row["OULevel"] != DBNull.Value)
                    bpm.OULevel = row["OULevel"].ToString();
                if (row["Code"] != DBNull.Value)
                    bpm.Code = row["Code"].ToString();
                if (row["OrderIndex"] != DBNull.Value)
                    bpm.OrderIndex = row["OrderIndex"].ToString();
                bpm.childers = GetRecoveBpmUser(context, bpm.OUID);
                list.Add(bpm);
            }
            return list;
        }
        //  public static BpmUserInfo 

    }
    public class RootBpmUser
    {
        public string OUID { get; set; }
        public string OUNAME { get; set; }
        public string ParentId { get; set; }
        public string OULevel { get; set; }
        public string Code { get; set; }
        public string OrderIndex { get; set; }
        public List<RootBpmUser> childers { get; set; }

    }
    public class BpmUserInfo
    {
        public string OUID { get; set; }
        public string OUNAME { get; set; }
        public string ParentId { get; set; }
        public string OULevel { get; set; }
        public string Code { get; set; }
        public int OrderIndex { get; set; }

    }
}
   