using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.CommandBuilder
{
    public class CommandRunner
    {
        private static Lazy<CommandRunner> runer = new Lazy<CommandRunner>(() => new CommandRunner());
        public static CommandRunner Instance => runer.Value;
        public ICommandExecutetor CommandExecutetor { get; set; }
        public CommandRunner()
        {
            if (CommandExecutetor == null)
                CommandExecutetor = new CommanndExecutetor();
        }
    }
}
