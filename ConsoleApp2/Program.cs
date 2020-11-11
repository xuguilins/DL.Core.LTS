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
using System.Globalization;
using DL.Core.ulitity.tools;
using System.Reflection;

namespace ConsoleApp2
{
    class Program
    {
        //  static List<RootBpmUser> list = new List<RootBpmUser>();
        static void Main(string[] args)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Age",typeof(int));
            dt.Columns.Add("Pass");
            DataRow row = dt.NewRow();
            row["Name"] = "语文";
            row["Age"] = 19;
            row["Pass"] = "1234";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["Name"] = "语文2";
            row["Age"] = 19;
            row["Pass"] = "111";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["Name"] = "语文8";
            row["Age"] = 19;
            row["Pass"] = "ddddd";
            dt.Rows.Add(row);
            var type = typeof(UserData);
            var props = type.GetProperties();
            foreach (PropertyInfo item in props)
            {
                

            }
            Console.ReadKey();
        }
     
        public static Tuple<DateTime, DateTime> GetFirstEndDayOfWeek(int year, int weekNumber, System.Globalization.CultureInfo culture)
        {
            System.Globalization.Calendar calendar = culture.Calendar;
            DateTime firstOfYear = new DateTime(year, 1, 1, calendar);
            DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber - 1);
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            while (targetDay.DayOfWeek != firstDayOfWeek)
            {
                targetDay = targetDay.AddDays(-1);
            }

            return Tuple.Create<DateTime, DateTime>(targetDay, targetDay.AddDays(6));
        }
        public static int GetYearWeekCount(int strYear)
        {
            System.DateTime fDt = DateTime.Parse(strYear.ToString() + "-01-01");
            int k = Convert.ToInt32(fDt.DayOfWeek);//得到该年的第一天是周几 
            if (k == 1)
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 1;
                return countWeek;

            }
            else
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 2;
                return countWeek;
            }

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
    public class UserData
    {
        public string Name { get; set; }
        public int Age { get; }

        public string Pass { get; set; }
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
   