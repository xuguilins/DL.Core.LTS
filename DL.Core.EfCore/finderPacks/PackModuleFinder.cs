using DL.Core.EfCore.packBase;
using DL.Core.ulitity.finder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.EfCore.finderPacks
{
    public interface IPackModuleFinder:IFinderBase<Type>
    {

    }
    public class PackModuleFinder : FinderBase<Type>, IPackModuleFinder
    {
        public override FinderType FinderType => FinderType.PackMoudelFinder;
        public override List<Type> Finder(Func<Type, bool> expression)
        {
            return FinderAll().Where(expression).ToList();
        }

        public override List<Type> FinderAll()
        {
            return LoadTypes.Where(x => !x.IsAbstract && !x.IsInterface && typeof(PackModule).IsAssignableFrom(x)).ToList();
        }
    }
}
