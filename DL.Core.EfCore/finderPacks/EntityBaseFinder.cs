﻿using DL.Core.ulitity.finder;
using DL.Core.ulitity.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Core.EfCore.finderPacks
{
    public interface IEntityBaseFinder : IFinderBase<Type>
    {

    }
    public class EntityBaseFinder :FinderBase<Type>, IEntityBaseFinder
    {
        public override FinderType FinderType => FinderType.EnttiyFinder;
        public override List<Type> Finder(Func<Type, bool> expression)
        {
            return FinderAll().Where(expression).ToList();
        }

        public override List<Type> FinderAll()
        {
            return LoadTypes.Where(x => !x.IsAbstract && !x.IsInterface && typeof(EntityBase) != x && typeof(EntityBase).IsAssignableFrom(x)).ToList();
        }
    }
}
