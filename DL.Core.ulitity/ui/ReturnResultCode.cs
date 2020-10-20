using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.ui
{
    public enum ReturnResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
       Success = 0,
       /// <summary>
       /// 错误
       /// </summary>
       Error = 1,
       /// <summary>
       /// 失败
       /// </summary>
       Failed = 2,
       /// <summary>
       /// 过期
       /// </summary>
       Expire = 3,
       /// <summary>
       /// 警告
       /// </summary>
       Warn = 4,
       /// <summary>
       /// 无任何变化
       /// </summary>
       NoChange = 5,
       /// <summary>
       /// 未知
       /// </summary>
       UnKnow = 9
    }
}
