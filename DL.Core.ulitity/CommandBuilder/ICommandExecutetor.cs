using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.ulitity.CommandBuilder
{
    /// <summary>
    /// 命令执行者接口
    /// </summary>
    public interface ICommandExecutetor
    {
        T Execute<T>(ICommand<T> command, object obj = null) where T : class, new();
        Task<T> ExecuteAsync<T>(ICommand<T> command, object obj = null) where T : class, new();
    }
}
