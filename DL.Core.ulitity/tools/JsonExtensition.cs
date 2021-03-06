﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.tools
{
    /// <summary>
    /// Json操作类库
    /// </summary>
    public static class JsonExtensition
    {
        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToJson(this object data) => JsonConvert.SerializeObject(data);

        /// <summary>
        /// json字符串转对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}