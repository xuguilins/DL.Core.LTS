using System;
using System.Collections.Generic;
using System.Linq;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity.ui;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.finder;
using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DL.Core.ns.EFCore;
using DL.Core.EfCore.engine;
using System.Runtime.CompilerServices;
using DL.Core.ulitity.attubites;
using DL.Core.ulitity.configer;
using DL.Core.ulitity.log;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ISqlServerDbContext context = new SqlServerDbContext();
            context.CreateDbConnection("Data Source=.;Initial Catalog=TestEngine;Integrated Security=True");
            context.AutoInitDataBaseTable();




            //var a = XmlConfigManager.Instance.GetSetting("appid");
            //var b = XmlConfigManager.Instance.GetHost("appurl");
            // Console.WriteLine($"a:{a},b:{b}");
            // logger.Debug("sdfsdf");
            //ConfigManager dc = new ConfigManager();
            //  var d = ConfigManager.Build.Mail;
            // var c = ConfigManager.Instance.ConnectionString;
            // IServiceCollection services = new ServiceCollection();
            // services.AddEnginePack<MyContext>();

            // var service = ServiceLocator.Instace.GetService(typeof(IUserService)) as IUserService;
            // service.CreateUser(new UserInfo { });

            //services.AddDbContext<MyContext>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //var provider = services.BuildServiceProvider();
            //var context = provider.GetService<MyContext>();
            //var service = provider.GetService<IUserService>();

            //service.CreateUser(new UserInfo { });



            Console.ReadKey();
        }
    }
    public interface IUserService
    {
        void CreateUser(UserInfo userInfo);
    }
    [DependencyAttbuite(ServiceLifetime.Scoped)]
    public class UserService : IUserService
    {
        private IRepository<UserInfo> userRepository;
         public UserService(IRepository<UserInfo> serRepository)
        {
            userRepository = serRepository;
       }
        public void CreateUser(UserInfo userInfo)
        {
            //userRepository.UnitOfWork.BeginTransaction = true;
            //userRepository.AddEntity(new UserInfo { UserName = "dddddsdfsfsf" });
            //userRepository.UnitOfWork.CommitTransaction();
            //throw new NotImplementedException();
        }
    }
    [TableAttubite("UserData")]
    public class UserInfo:EntityBase
    {
        public string UserName { get; set; }

        public bool IsEnable { get; set; }
        //public string UserPass { get; set; }

    }
    [TableAttubite("StudentInfo")]
    public class StduentInfo:EntityBase
    {
        [ColummLengthAttbuite(100)]
        public string StudentName { get; set; }
    }

    public class MyContext : DbContextBase<MyContext>
    {
      
        public override string ConnectionString => "Data Source=.;Initial Catalog=Test_T;Integrated Security=True";
        
    }
    public class UserConfiguration : ConfigurationBase<UserInfo>
    {
        public override Type DbContextType => typeof(MyContext);

        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("UserInfo");
        }
    }
    public class StduentInfoConfiguration:ConfigurationBase<StduentInfo>
    {
        public override Type DbContextType => typeof(MyContext);

        public override void Configure(EntityTypeBuilder<StduentInfo> builder)
        {
            builder.ToTable("StduentInfo");
        }
    }
}
