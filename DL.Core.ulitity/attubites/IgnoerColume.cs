using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.attubites
{
    public class IgnoerColume:Attribute
    {
        public bool Ignore { get; set; }
        /// <summary>
        /// 忽略列
        /// </summary>
        /// <param name="isIgnoer"></param>
        public IgnoerColume(bool isIgnoer)
        {
            Ignore = isIgnoer;
        }
    }
}
