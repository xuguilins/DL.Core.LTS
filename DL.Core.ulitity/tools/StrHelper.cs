using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace DL.Core.ulitity.tools
{
  
    public static class StrHelper
    {
  
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime() => DateTime.Now;

        /// <summary>
        /// 获取时间信息（可格式化）
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static string GetDataTime(string formatter = "yyyy-MM-dd") => DateTime.Now.ToString(formatter);

        /// <summary>
        /// 获取guid，（可格式化）
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static string GetGuid(string formatter = "N") => Guid.NewGuid().ToString(formatter);
        /// <summary>
        /// 获取时间排序的GUID
        /// </summary>
        /// <returns></returns>
        public static string GetDateGuid()
        {
            var time = GetDataTime("ddHHmm");
            long i = 1;
            foreach (var bt in Guid.NewGuid().ToByteArray())
                i *= bt + 1;
            var result = string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper();
            return time + result;
        }

        /// <summary>
        /// 获取可排序的GUID
        /// </summary>
        /// <returns></returns>
        public static string GetXGuid()
        {
            var guid = Guid.NewGuid().ToString("N").ToUpper();
            var bytes = Encoding.UTF8.GetBytes(guid);
            Array.Reverse(bytes);
            string result = string.Empty;
            for (int j = 0; j < 2; j++)
            {
                result += bytes[j];
            }
            var uid = guid.ExpenSubstr(15);
            return result + uid;
        }
        /// <summary>
        /// 获取当前时间是今年的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime dt)
        {
           
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }
        /// <summary>
        /// 获取当前时间的当前周的周范围
        /// </summary>
        /// <returns></returns>
        public static string GetWeekRange(DateTime time)
        {
            GregorianCalendar gc = new GregorianCalendar();
            var week = gc.GetDayOfWeek(time);
            var nowTime = time;
            DateTime starttime = DateTime.Now;
            while (week != DayOfWeek.Monday)
            {
                starttime = nowTime.AddDays(-1);
                week = starttime.DayOfWeek;
                nowTime = starttime;
            }
            var endTime = starttime.AddDays(6);
            var startStr = starttime.ToString("yyyy-MM-dd");
            var endStr = endTime.ToString("yyyy-MM-dd");
            return startStr + "~" + endStr;
           
        }
        /// <summary>
        /// 获取指定年份共有多少周
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns></returns>
        public static int GetYearWeekCount(int strYear)
        {
              DateTime fDt = DateTime.Parse(strYear.ToString() + "-01-01");
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
        /// <summary>
        /// 获取指定时间段有多少周
        /// </summary>
        /// <param name="startiem"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public static int GetYearWeekCount(DateTime startiem,DateTime endtime)
        {
            var day = (startiem - endtime).TotalDays; 
            int seekday = (day / 7).ToInt32();
            int errday = day.ToInt32() - (seekday * 7);
            if (errday > 0)
            {
                int endweek = seekday + errday;
                return endweek;
            }
            return seekday;
        }
        /// <summary>
        /// 根据指定周获取周范围
        /// </summary>
        /// <param name="week">周数</param>
        /// <returns></returns>
        public static string GetWeekRangeByWeek(int week)
        {
            //获取当前周
            var nowWeek = GetWeekOfYear(DateTime.Now);
            var nowWeekRange = GetWeekRange(DateTime.Now);
            string result = string.Empty;
              if (week > nowWeek)
                {
                   //计算相差多少周
                  int seekweek = week - nowWeek;
                  int seekday = seekweek * 7;
                  DateTime startime = nowWeekRange.Split('~')[0].ToDateTime().AddDays(seekday);
                  DateTime endtime = nowWeekRange.Split('~')[1].ToDateTime().AddDays(seekday);
                  result = $"{startime.ToFormattertime()}~{endtime.ToFormattertime()}";
                }
                else
                {
                    int seekweek = week - nowWeek;
                    int seekday = seekweek * 7;
                    DateTime startime = nowWeekRange.Split('~')[0].ToDateTime().AddDays(seekday);
                    DateTime endtime = nowWeekRange.Split('~')[1].ToDateTime().AddDays(seekday);
                    if(startime.Year<DateTime.Now.Year)
                    {
                        int year = startime.Year;
                        while (year<DateTime.Now.Year)
                        {
                            startime = startime.AddDays(1);
                            year = startime.Year;
                        }
                    }
                    result = $"{startime.ToFormattertime()}~{endtime.ToFormattertime()}";
                } 
            return result;
        }
    }
}
