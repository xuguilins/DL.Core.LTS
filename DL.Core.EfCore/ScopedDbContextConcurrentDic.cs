using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.EfCore
{
    public class ScopedDbContextConcurrentDic: ConcurrentDictionary<string,IDbContext>
    {
        
    }
}
