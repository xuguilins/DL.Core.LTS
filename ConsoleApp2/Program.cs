using System;
using System.Collections.Generic;
using System.Linq;
using DL.Core.Ado.SqlServer;
using DL.Core.ulitity.ui;
using DL.Core.ulitity.log;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.ulitity.finder;
using DL.Core.EfCore.finderPacks;
using DL.Core.EfCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DL.Core.ns.EFCore;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IEntityBaseFinder, EntityBaseFinder>();
            services.AddScoped<IEntityConfigurationFinder, EntityConfigurationFinder>();
            services.AddDbContext<MyContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            var provider = services.BuildServiceProvider();
            var context = provider.GetService<MyContext>();
            var service = provider.GetService<IUserService>();
         
            service.CreateUser(new UserInfo { });


            Console.ReadKey();
        }
    }
    public interface IUserService
    {
        void CreateUser(UserInfo userInfo);
    }
    public class UserService : IUserService
    {
        private IRepository<UserInfo> userRepository;
         public UserService(IRepository<UserInfo> serRepository)
        {
            userRepository = serRepository;
       }
        public void CreateUser(UserInfo userInfo)
        {
            userRepository.UnitOfWork.BeginTransaction = true;
            userRepository.AddEntity(new UserInfo { UserName = "奥斯卡印sdfsdfsdf地" });
            userRepository.UnitOfWork.CommitTransaction();
           // throw new NotImplementedException();
        }
    }
    public class UserInfo:EntityBase
    {
        public string UserName { get; set; }

    }
    public class TTS:EntityBase
    {

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
    public class UserseConfiguration : ConfigurationBase<TTS>
    {
        public override Type DbContextType => typeof(MyContext);

        public override void Configure(EntityTypeBuilder<TTS> builder)
        {
            builder.ToTable("TTS");
        }
    }

}
