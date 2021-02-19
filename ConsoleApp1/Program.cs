using System;
using DL.Core.EfCore;
using DL.Core.EfCore.SqlServer;
using DL.Core.ulitity.ui;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using DL.Core.EfCore.engine;
using DL.Core.ulitity.attubites;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.EnableAutoMigration(false);
            services.AddEnginePack<MyContext>();
             
            var service = ServiceLocator.Instace.GetService<IUserService>();
            service.AddUser();
            Console.ReadKey();
        }
    }
    public class MyContext : DefaultDbContext<MyContext>
    {
        public override string ConnectionString => "Data Source=.;Initial Catalog=Test_T;Integrated Security=True";
    }
    public class UserInfo : EntityBase
    {
        public string UserName { get; set; }
    }
    public class UserInfoConfiguration : ConfigurationBase<UserInfo>
    {
        public override Type DbContextType => typeof(MyContext);
        public override void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.UserName).HasMaxLength(50);
            builder.Property(m => m.CreatedTime).HasMaxLength(50);
        }
    }

    public interface IUserService : IScopeDependcy
    {
        void AddUser();
    }
    public class UserService : IUserService
    {
        private IRepository<UserInfo> _userRespository;
        public UserService(IRepository<UserInfo> repository)
        {
            _userRespository = repository;
        }
        public void AddUser()
        {
            var abc= _userRespository.AddEntity(new UserInfo
            { 
                 UserName = "奥里给"
            });
            Console.WriteLine($"输出结果：{abc}");
        }
    }
}
