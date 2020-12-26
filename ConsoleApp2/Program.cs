
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

namespace ConsoleApp2
{
    internal class Program
    {


        private static void Main(string[] args)
        {
            List<UserInfo> list = new List<UserInfo>();
            for (int i = 0; i < 1300; i++)
            {
                list.Add(new UserInfo { CreateUser = "abc", PassWord = i.ToString(), UserCount = i, UserName = "AOO" + i });
            }
            ISqlServerDbContext context = new SqlServerDbContext();
            context.CreateDbConnection("Data Source=.;Initial Catalog=test_sqlbusiness;User ID=sa;Password=0103");
            context.InsertItems(list);

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

