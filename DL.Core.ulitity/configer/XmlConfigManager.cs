using DL.Core.ulitity.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DL.Core.ulitity.configer
{
    public  class XmlConfigManager
    {
        private static List<XmlNode> hostList = new List<XmlNode>();
        private static List<XmlNode> SetList = new List<XmlNode>();
        private static bool isLoad =false;
        private static ILogger logger = LogManager.GetLogger<XmlConfigManager>();
        public XmlConfigManager()
        {
            if (!isLoad)
             InitXmlData();
        }  
        private void InitXmlData()
        {
           isLoad = true;
            string path = ConfigManager.Build.GetDLSetting("XmlPath");
            if (string.IsNullOrWhiteSpace(path))
                path = AppDomain.CurrentDomain.BaseDirectory;
            logger.Info($"xml文件读取路径：{path}");
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlElement elment = xml.DocumentElement;
            var node = elment.SelectSingleNode("/DLConfig/Host");
            if (node!=null)
            {
                var childs = node.ChildNodes;
                if (childs!=null && childs.Count>0)
                {
                    foreach (XmlNode item in childs)
                    {
                        hostList.Add(item);
                    }
                }
            }
            var setNode = elment.SelectSingleNode("/DLConfig/Setting");
            if(setNode!=null)
            {
                var childs = setNode.ChildNodes;
               if (childs!=null && childs.Count>0)
                {
                    foreach (XmlNode item in childs)
                    {
                        SetList.Add(item);
                    }
                }
            }
        }
        public static XmlConfigManager Instance => new XmlConfigManager();
        public string GetSetting(string keyName)
        {
            return  SetList.FirstOrDefault(x => x.Attributes["key"]?.Value == keyName)?.Attributes["value"].Value;
        }
        public string GetHost(string keyName)
        {
            return hostList.FirstOrDefault(x => x.Attributes["key"]?.Value == keyName)?.Attributes["value"].Value;
        }      
    }
}

