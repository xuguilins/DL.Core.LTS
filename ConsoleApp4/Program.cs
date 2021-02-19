using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using DL.Core.Ado;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity;
using DL.Core.ulitity.attubites;
using DL.Core.ulitity.ui;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {

            

            Console.ReadKey();
        }
    }
    public class UserInfo:EntityBase
    {
        public string UserName { get; set; }   
    }
}
