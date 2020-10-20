using System;
using System.Collections.Generic;
using System.Text;
using DL.Core.ulitity.tools;
namespace DL.Core.ulitity.ui
{
    public class EntityBase
    {
        public string Id { get; set; } = StrHelper .GetDateGuid();

        public DateTime CreatedTime { get; set; } = StrHelper.GetDateTime();
    }
}
