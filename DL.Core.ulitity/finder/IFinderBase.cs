using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DL.Core.ulitity.finder
{
    public interface IFinderBase<T> 
    {
        /// <summary>
        /// 所有程序集类型
        /// </summary>
         List<Type> LoadTypes { get; }
        /// <summary>
        /// 查找的类型
        /// </summary>
        FinderType FinderType { get; }
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <returns></returns>
        List<T> FinderAll();
        /// <summary>
        /// 查找指定
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<T> Finder(Func<T, bool> expression);

    }
}
