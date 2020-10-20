using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Core.ulitity.finder;
namespace DL.Core.EfCore.finderPacks
{
    public interface IEntityConfigurationFinder : IFinderBase<Type>
    {

    }
    public class EntityConfigurationFinder : FinderBase<Type>, IEntityConfigurationFinder
    {
        public override List<Type> Finder(Func<Type, bool> expression)
        {
            return FinderAll().Where(expression).ToList();
        }

        public override List<Type> FinderAll()
        {
            return LoadTypes.Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEntityTypeRegiest).IsAssignableFrom(x))
                 .ToList();
        }
    }
}
