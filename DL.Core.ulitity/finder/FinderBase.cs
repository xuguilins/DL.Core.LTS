using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DL.Core.ulitity.finder
{
    public abstract class FinderBase<T> : IFinderBase<T>
    {
        public FinderBase()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
           .ToArray();
            var assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();
            LoadTypes = types;
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        public List<Type> LoadTypes { get; private set; }
        public virtual FinderType FinderType { get; private set; }

        public abstract List<T> Finder(Func<T, bool> expression);

        public abstract List<T> FinderAll();



         
    }
}
