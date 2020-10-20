using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.attubites
{
    public class TableAttubite:Attribute
    {
        public string TableName { get; set; }
        public TableAttubite(string tableName)
        {
            TableName = tableName;
        }
    }
}
