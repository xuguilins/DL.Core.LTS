using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ConsoleApp2
{
    public class XmlService
    {
        public XmlNodeList LoadXml()
        {
            var path = @"C:\Users\陶翔荣\source\repos\DL.Core.LTS\DL.Core.ulitity\DLConfig.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlElement elment = xml.DocumentElement;
            var node = elment.SelectSingleNode("/DLConfig/Host");
            var childs = node.ChildNodes;
            return childs;
            //foreach (XmlNode xmlNode in childs)
            //{
            //    var d = xmlNode.Attributes;
            //    var key = xmlNode.Attributes["key"];
            //    var value = xmlNode.Attributes["value"];
            //    var name = xmlNode.Name;

            //}

        }

        public string GetConfig(string name)
        {
            var nodes = LoadXml();
            List<XmlNode> list = new List<XmlNode>();
            foreach (XmlNode node in nodes)
            {
                list.Add(node);
            }

            var item = list.FirstOrDefault(x => x.Attributes["key"]?.Value == name);
            if (item != null)
            {
                return item.Attributes["value"].Value;
            } else
            {
                return string.Empty;
            }
        
        }
    }
}
