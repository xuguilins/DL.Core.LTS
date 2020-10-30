using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        //static List<BaseInfo> baseList = new List<BaseInfo>();
        static List<UserInfo> userList = null;
        static void Main(string[] args)
        {
            List<UserInfo> list = new List<UserInfo>
            {
               new UserInfo{ DepartName = "开发部", Id="D1001", ParentId =null },
               new UserInfo{ DepartName = "开发A部门", Id="D1002", ParentId ="D1001" },
               new UserInfo{ DepartName = "开发B部门", Id="D1003", ParentId ="D1001" },
               new UserInfo{ DepartName = "开发C部门", Id="D1004", ParentId ="D1001" },
               new UserInfo{ DepartName = "开发C部门-1",  Id="D10041", ParentId="D1004" },
               new UserInfo{ DepartName="研发部门", Id="D2001", ParentId=null },
               new UserInfo{ DepartName  ="研发A部门", Id="D2002",ParentId="D2001" }
            };
            userList = list;
            var xdata = GetDepartMent(null);
            Console.ReadKey();
        }
        public static List<BaseInfo> GetDepartMent(string parentId = null)
        {
           
            List<BaseInfo> baseList = new List<BaseInfo>();
            List<UserInfo> datalist = null;
            if (parentId == null)
            {
                datalist = userList.Where(x => x.ParentId == parentId).ToList();
            }
            else
            {
                datalist = userList.Where(x => x.ParentId == parentId).ToList();
            }
            foreach (var item in datalist)
            {
                baseList.Add(new BaseInfo
                {
                    DepartName = item.DepartName,
                    Id = item.Id,
                    Children = GetDepartMent(item.Id)
                }); ;

            }
            return baseList;

        }

    }
    class UserInfo
    {
        public string Id { get; set; }
        public string DepartName { get; set; }
        public string ParentId { get; set; }
    }
    public class BaseInfo
    {
        public string Id { get; set; }
        public string DepartName { get; set; }
        public List<BaseInfo> Children { get; set; }
    }
    public class ChildInfo
    {
        public string Id { get; set; }
        public string DepartName { get; set; }
    }
}
