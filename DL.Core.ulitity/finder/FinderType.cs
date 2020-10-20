using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.finder
{
    public enum FinderType
    {
        /// <summary>
        /// 模块包查找
        /// </summary>
        PackMoudelFinder = 0,
        /// <summary>
        /// 注入查找
        /// </summary>
        DepenencyFinder = 1,
        /// <summary>
        /// 上下文查找
        /// </summary>
        ContextFinder =2,
        /// <summary>
        /// 实体查找
        /// </summary>
        EnttiyFinder =3,
        /// <summary>
        /// 实体配置查找
        /// </summary>
        EnttiyConfigFinder = 4,

    }
}
