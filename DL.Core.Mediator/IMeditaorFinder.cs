using DL.Core.ulitity.finder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.Mediator
{
    public interface IMeditaorFinder:IFinderBase<Type>
    {
    }
    public class MeditaorFinder : FinderBase<Type>, IMeditaorFinder
    {
        public override FinderType FinderType => FinderType.MeditaorFinder;
        public override List<Type> Finder(Func<Type, bool> expression)
        {
            return FinderAll().Where(expression).ToList();
        }

        public override List<Type> FinderAll()
        {
            var types= LoadTypes.Where(m => !m.IsAbstract && !m.IsInterface && !m.IsSealed && (typeof(NotifyHandler).IsAssignableFrom(m) || (typeof(IRequestExecute).IsAssignableFrom(m)))).ToList();
            return types;
        }
    }
}
