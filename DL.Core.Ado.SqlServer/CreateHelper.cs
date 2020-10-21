using DL.Core.ulitity.attubites;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DL.Core.Ado.SqlServer
{
    public static class CreateHelper
    {
        /// <summary>
        /// 获取字段长度
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int GetLength(PropertyInfo item)
        {
            int length = 100;
            var attbuite = item.GetCustomAttributes(false);
            if (attbuite != null && attbuite.Length > 0)
            {
                var attb = attbuite[0] as ColummLengthAttbuite;
                length = attb.Length;
            }
            return length;
        }
        /// <summary>
        /// 解析字段类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static string ParsePropType(string typeName, string length)
        {
            string result = string.Empty;
            switch (typeName)
            {
                case "String":
                    result = (string.IsNullOrWhiteSpace(length) ? "varchar(100)" : $"varchar({length})");
                    break;

                case "Int32":
                    result = "int";
                    break;

                case "Decimal":
                    result = (string.IsNullOrWhiteSpace(length) ? "decimal(18, 2)" : $"decimal({length})");
                    break;

                case "DateTime":
                    result = "datetime";
                    break;
            }
            return result;
        }
    }
}
