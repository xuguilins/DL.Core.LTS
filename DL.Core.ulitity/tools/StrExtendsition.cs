using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DL.Core.ulitity.tools
{
    public static class StrExtendsition
    {
        /// <summary>
        /// 字符转INT
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToInt32(this object data) => Convert.ToInt32(data);

        /// <summary>
        /// 字符串转INT
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToInt16(this object data) => Convert.ToInt16(data);

        /// <summary>
        /// 字符串转金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object data) => Convert.ToDecimal(data);

        /// <summary>
        /// 字符串转单精度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float ToFloat(this object data) => Convert.ToSingle(data);

        /// <summary>
        /// 字符串转双精度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double ToDuble(this object data) => Convert.ToDouble(data);
    
        public static string ToFormattertime(this DateTime time,string formatter= "yyyy-MM-dd")=>time.ToString(formatter);
        /// <summary>
        /// 日期转换
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string data) => Convert.ToDateTime(data);
        /// <summary>
        /// 字符串分割为成数组
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="fix">分割的字符,默认为【,】</param>
        /// <returns></returns>
        public static string[] ExpenstrToarry(this string data, char fix = ',') => data.Split(fix);

        /// <summary>
        /// 数组转字符串
        /// </summary>
        /// <param name="arry">字符串数组</param>
        /// <param name="fix">分隔的字符默认为【,】</param>
        /// <returns></returns>
        public static string ArryToStr(this string[] arry, string fix = ",") => string.Join(fix, arry);

        /// <summary>
        /// 集合转字符串
        /// </summary>
        /// <param name="list">字符串集合</param>
        /// <param name="fix">拼接字符,默认为【,】</param>
        /// <returns></returns>
        public static string ListToStr(this List<string> list, string fix = ",") => string.Join(fix, list.Select(x => x.ToString()));

        /// <summary>
        /// 移除字符串中后面的X个字符
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ExpenSubstr(this string data, int count = 1) => data.Length > count ? data.Substring(0, data.Length - count) : "移除失败,移除字符个数大于当前字符串长度";
        // <summary>
        /// 仅支持对象T转字典,不允许对象嵌套
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this object value)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var propList = value.GetType().GetProperties();
            foreach (var item in propList)
            {
                var name = item.Name;
                var val = item.GetValue(value, null);
                dic.Add(name, val);
            }
            return dic;
        }

        /// <summary>
        /// 字典转对象,仅支持一级
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToObject<T>(this Dictionary<string, object> value) where T : class, new()
        {
            var model = new T();
            var propList = model.GetType().GetProperties();
            foreach (var item in value)
            {
                var info = propList.FirstOrDefault(m => m.Name.Equals(item.Key));
                if (info != null)
                {
                    info.SetValue(model, item.Value);
                }
            }
            return model;
        }
  
        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="parms"></param>
        public static void ChekcNotNull(this string parms)
        {
            if (string.IsNullOrWhiteSpace(parms))
                throw new ArgumentNullException($"参数不能为空");
        }

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="parms"></param>
        public static void ChekcNotNull(this string parms, string data)
        {
            if (string.IsNullOrWhiteSpace(parms))
                throw new ArgumentNullException($"参数{data}不能为空");
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="parms">参数</param>
        public static void CheckListNotNull<T>(this List<T> parms)
        {
            if (!parms.Any())
                throw new Exception($"集合不能为空引用或集合数量不能为0");
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        public static bool CheckIEnumerable<T>(this IEnumerable<T> parms)
        {
            return parms.Any();
        }

        /// <summary>
        /// 检查GUID是否为空
        /// </summary>
        /// <param name="guid"></param>
        public static void CheckGuidNotNull(this Guid guid)
        {
            if (guid == Guid.Empty)
                throw new Exception($"GUID不能为空");
        }

        /// <summary>
        /// 检查字典集合是否为空
        /// </summary>
        /// <param name="parms"></param>
        public static bool CheckDictionaryNotNull(this Dictionary<string, object> parms)
        {
            return parms.Any();
        }

        /// <summary>
        /// 检查对象是否为空
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">对象</param>
        public static void CheckObjectNotNull<T>(this T data)
        {
            if (data == null)
                throw new Exception($"对象不能为空引用");
        }

        /// <summary>
        /// 检查是否为空
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckPamrsIsNotNull(this string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }



        /// <summary>
        /// Strem转字节
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToByte(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 数据转字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToByte(this string data) => Encoding.UTF8.GetBytes(data);

        /// <summary>
        /// 转字节流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static MemoryStream ToStream(this byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            return ms;
        } 
        /// <summary>
        /// 小写金额转大写
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public static string ToMoneyUpper(this decimal money)
        {
            var s = money.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r + "整";
        }

    }
}
