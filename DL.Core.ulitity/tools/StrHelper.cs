using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
        private static int GetWeekOfYear(DateTime dt)
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
            return startStr + "-" + endStr;
           
        }
    }
}
