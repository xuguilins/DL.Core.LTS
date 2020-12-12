using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.CommandBuilder
{
    /// <summary>
    /// 命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommand<T> where T : class, new()
    {
        T Execute(object data = null);
    }

}
