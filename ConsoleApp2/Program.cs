
using DL.Core.EfCore;
using DL.Core.EfCore.engine;
using DL.Core.EfCore.SqlServer;
using DL.Core.ulitity.ui;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using DL.Core.ulitity.attubites;
using DL.Core.EfCore.MySql;
using System.Collections.Generic;
using DL.Core.Ado.SqlServer;
using DL.Core.Ado.Oracle;
using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace ConsoleApp2
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            var conStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.100.67.13)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=C##ZTASK;Password=Lp!X8mPIU!PFLqmi";
            IOraceDbContext context = new OraceDbContext();
            context.CreateDbConnection(conStr);
            //查询
            //var seachSql = "SELECT * FROM TFTASK_WEEKTIMEINFO";
            //var table= context.GetDataTable(seachSql, System.Data.CommandType.Text);
            //修改
            context.BeginTransaction = true;
            OracleParameter[] ps =
            {
                new OracleParameter("NOWYEAR","20225"),
                new OracleParameter("ID",63)
            };
           var updateSql = "update TFTASK_WEEKTIMEINFO SET NOWYEAR=:NOWYEAR WHERE ID=:ID";
            var count= context.ExecuteNonQuery(updateSql, CommandType.Text,ps);
            context.SaveTransactionChange();

            Console.ReadKey();
        }
    }
    public class MySqlDbContext : MySqlDefaultDbContext<MySqlDbContext>
    {

    }
    public class UserInfo : EntityBase
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int UserCount { get; set; }
        public string CreateUser { get; set; }
    }
    public class UserInfoEntityConfiguration : ConfigurationBase<UserInfo>
    {
        public override Type DbContextType => typeof(MySqlDbContext);

        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PassWord).HasMaxLength(50);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.CreatedTime).HasMaxLength(20);
            builder.Property(x => x.UserCount).HasMaxLength(10);
            builder.Property(x => x.CreateUser).HasMaxLength(50);




        }
    }

    public interface IUserService : IScopeDependcy
    {
        void CreateUserInfo(UserInfo info);
    }
    public class UserService : IUserService
    {
        private IRepository<UserInfo> _userReposiroty;
        public UserService(IRepository<UserInfo> repository)
        {
            _userReposiroty = repository;
        }
        public void CreateUserInfo(UserInfo info)
        {
            var abc = _userReposiroty.AddEntity(info);
        }

    }
}

