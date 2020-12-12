using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.ulitity.CommandBuilder
{
    /// <summary>
    /// 命令执行者
    /// </summary>
    public class CommanndExecutetor : ICommandExecutetor
    {
        public T Execute<T>(ICommand<T> command, object obj = null) where T : class, new()
        {
            var result = command.Execute(obj);
            return result;
        }
        public async Task<T> ExecuteAsync<T>(ICommand<T> command, object obj = null) where T : class, new()
        {
            var result = await Task.Factory.StartNew(() =>
            {
                return command.Execute(obj);
            });
            return result;
        }
    }
}
