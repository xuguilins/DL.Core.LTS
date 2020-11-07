using System;
using System.Collections.Generic;
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
        /// 返回Guid用于数据库操作，特定的时间代码可以提高检索效率
        /// </summary>
        /// <returns>COMB类型 Guid 数据</returns>
        public static Guid NewGuid()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime dtBase = new DateTime(1900, 1, 1);
            DateTime dtNow = DateTime.Now;
            //获取用于生成byte字符串的天数与毫秒数
            TimeSpan days = new TimeSpan(dtNow.Ticks - dtBase.Ticks);
            TimeSpan msecs = new TimeSpan(dtNow.Ticks - new DateTime(dtNow.Year, dtNow.Month, dtNow.Day).Ticks);
            //转换成byte数组
            //注意SqlServer的时间计数只能精确到1/300秒
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //反转字节以符合SqlServer的排序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //把字节复制到Guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
    }
}
