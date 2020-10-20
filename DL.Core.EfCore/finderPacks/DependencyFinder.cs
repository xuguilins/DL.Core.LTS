using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DL.Core.ulitity.attubites;
using DL.Core.ulitity.finder;
using System.Reflection;

namespace DL.Core.EfCore.finderPacks
{
    public interface IDependencyFinder : IFinderBase<Type>
    {

    }
    public class DependencyFinder : FinderBase<Type>, IDependencyFinder
    {
        public Type[] DependencyType => new Type[] { typeof(IScopeDependcy), typeof(ISingletonDependcy), typeof(ITransientDependcy) };


        public override FinderType FinderType => FinderType.DepenencyFinder;

        public override List<Type> Finder(Func<Type, bool> expression)
        {
            return FinderAll().Where(expression).ToList();
        }

        public override List<Type> FinderAll()
        {
            var typeList = LoadTypes.Where(x => !x.IsAbstract && !x.IsInterface && !x.IsDefined(typeof(IgnoreDependencyAttbuite))
            && DependencyType.Any(m => m.IsAssignableFrom(x))).ToList();
            //查找类型：
            var attbuitList = LoadTypes.Where(x => !x.IsAbstract && !x.IsInterface && x.IsDefined(typeof(DependencyAttbuite))).ToList();
            typeList.AddRange(attbuitList);
            return typeList;

        }
    }
}
