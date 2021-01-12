using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ulitity.tools;
namespace DL.Core.ulitity.ui
{
    public class EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; } = StrHelper .GetDateGuid();

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = StrHelper.GetDateTime();
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }
    }
}
