using DL.Core.ulitity.finder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.ulitity.EventBusHandler
{
    public interface IEventBusFinder:IFinderBase<Type>
    {
    }
    public class EventBusFinder : FinderBase<Type>, IEventBusFinder
    {
        public override FinderType FinderType => FinderType.EventHandlerFinder;
        public override List<Type> Finder(Func<Type, bool> expression)
        {
            return FinderAll().Where(expression).ToList();
        }

        public override List<Type> FinderAll()
        {
            return LoadTypes.Where(x => !x.IsInterface && typeof(EventData) != x && !x.IsAbstract).ToList();
        }
    }
}
