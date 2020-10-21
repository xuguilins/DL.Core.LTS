using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.attubites
{
    public  class ColummLengthAttbuite:Attribute
    {
        public int Length { get; set; }
        public ColummLengthAttbuite(int length)
        {
            Length = length;
        }
    }

}
