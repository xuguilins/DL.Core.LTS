using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DL.Core.EfCore
{
    public class EntityConcurrentDictory:ConcurrentDictionary<string,Assembly>
    {
        public static readonly Lazy<EntityConcurrentDictory> layz = new Lazy<EntityConcurrentDictory>(() => new EntityConcurrentDictory());
        public static EntityConcurrentDictory Instacne => layz.Value;
    }
}

