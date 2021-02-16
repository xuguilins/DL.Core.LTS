using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DL.Core.ulitity.tools
{
    public static class StrExtendsition
    {
        /// <summary>
        /// 生成真正的随机数
        /// </summary>
        /// <param name="r"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static int StrictNext(this Random r, int seed = int.MaxValue)=> new Random((int)Stopwatch.GetTimestamp()).Next(seed);
        
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

        public static string ToFormattertime(this DateTime time, string formatter = "yyyy-MM-dd") => time.ToString(formatter);
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
        /// <summary>
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
        /// 手机号脱敏处理
        /// 如：183****3333
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string HiddenNumber(this string number)
        {
            var express = @"^[0-9]*$";
            var flag = Regex.IsMatch(number, express);
            int length = number.Length;
            string result = string.Empty;
            if (flag && length == 11)
            {
                var start = number.Substring(0, 3);
                var middler = "****";
                var end = number.Substring(7, 4);
                result = $"{start}{middler}{end}";
           
            }
            return result;
        }

        /// <summary>
        /// 将流转换为内存流
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static MemoryStream SaveAsMemoryStream(this Stream stream)
        {
            stream.Position = 0;
            return new MemoryStream(stream.ToArray());
        }

        /// <summary>
        /// byte数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
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
        /// <summary>
        /// 对象克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Clone<T>(this T data) where T : class, new()
        {
            var model = new T();
            var newModelProps = model.GetType().GetProperties().ToList();
            var dataProps = data.GetType().GetProperties();
            foreach (var prop in dataProps)
            {
                var newModel = newModelProps.FirstOrDefault(m => m.Name.Equals(prop.Name));
                if (newModel != null)
                {
                    var value = prop.GetValue(data, null);
                    if (prop.CanRead && prop.CanWrite)
                    {
                        newModel.SetValue(model, value);
                    }

                }
            }
            return model;
        }

        /// <summary>
        /// 把对象类型转换为指定类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }
            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 把对象类型转化为指定类型
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败引发异常。 </returns>
        public static T CastTo<T>(this object value)
        {
            if (value == null && default(T) == null)
            {
                return default(T);
            }
            if (value.GetType() == typeof(T))
            {
                return (T)value;
            }
            object result = CastTo(value, typeof(T));
            return (T)result;
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                return CastTo<T>(value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 对象转字典
        /// 仅支持一级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">对象数据</param>
        /// <returns></returns>
        public static Hashtable ToHashTable<T>(this T data) where T:class,new() 
        {
            var type = data.GetType();
            var propItems = type.GetProperties();
            Hashtable hs = new Hashtable();
            foreach (var item in propItems)
            {
                var value = item.GetValue(data, null);
                hs.Add(item.Name, value);
            }
            return hs;
        }
    }
}
