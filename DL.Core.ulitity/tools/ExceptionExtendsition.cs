using DL.Core.ulitity.log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.tools
{
    public class SqlServerException:Exception
    {
        private ILogger logger = LogManager.GetLogger<SqlServerException>();
        public SqlServerException()
        {

        }
     
        public SqlServerException(string message)
        {
            logger.Error($"【SqlServerException异常提醒】\r\nErrorMessage:[{message}]\r\nException:[{base.InnerException.InnerException?.StackTrace}]");
        }
    }
}
