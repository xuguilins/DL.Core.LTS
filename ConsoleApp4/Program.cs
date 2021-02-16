using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using DL.Core.Ado;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity;
using DL.Core.ulitity.attubites;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {

            //手机号脱敏
            string number = "18720294546";
            var express = @"^[0-9]*$";
            var flag = Regex.IsMatch(number, express);
            int length = number.Length;
            if (flag && length==11)
            {
                var start = number.Substring(0, 3);
                var middler = "****";
                var end = number.Substring(7, 4);
                var res = $"{start}{middler}{end}";
                Console.WriteLine(res);



            }




            Console.ReadKey();
        }
    }
    public class UserInfo
    {
        [IgnoerColume(true)]
        public int ID { get; set; }
        [Description("用户名")]
        public string USERNAME { get; set; }
        public int USERAGE { get; set; }
        public DateTime CREATETIME { get; set; }
        public string USERPASS { get; set; }
    }
}
