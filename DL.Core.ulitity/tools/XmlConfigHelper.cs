using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DL.Core.ulitity.tools
{
    public static class XmlConfigHelper
    {
        /// <summary>
        /// 加载XML文件
        /// </summary>
        /// <param name="filepath"></param>
        public static void LoadXml(string filepath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
        }
        /// <summary>
        /// 加载XML文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>返回XML元素对象</returns>
        public static XmlElement LoadElmentXml(string filepath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            return xml.DocumentElement;
        }

        /// <summary>
        /// 加载xml的指定节点
        /// </summary>
        /// <param name="filepath">xml文件路径</param>
        /// <param name="xmlPath">/xx/xx</param>
        /// <returns></returns>
        public static XmlNodeList LoadXmlNodeList(string filepath,string xmlPath)
        {
            var elment = LoadElmentXml(filepath);
            return elment.SelectNodes(xmlPath);
        }
    }
}
