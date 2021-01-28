using System;
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

            UserInfo info = new UserInfo
            {
               USERAGE = 12,
               USERNAME = "abc",
               USERPASS = "222",
               CREATETIME =DateTime.Now
            };
            ISqlServerDbContext context = new SqlServerDbContext();
            context.CreateDbConnection("Data Source=.;Initial Catalog=CodeFormDB;Persist Security Info=True;User ID=sa;Password=0103");
            context.Insert(info);
            Console.ReadKey();
        }
    }
    public class UserInfo
    {
        [IgnoerColume(true)]
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public int USERAGE { get; set; }
        public DateTime CREATETIME { get; set; }
        public string USERPASS { get; set; }
    }
}
