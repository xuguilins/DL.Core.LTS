using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Core.ulitity.web
{
    public class HttpType
    {

        #region [请求媒体类型]


        /// <summary>
        /// XHTML格式
        /// </summary>
        public const string XHtmlType = "application/xhtml+xml";
        /// <summary>
        /// XML格式
        /// </summary>
        public const string XmlType = "application/xml";
        /// <summary>
        /// Atom XML聚合格式  
        /// </summary>
        public const string AtomXmlType = "application/atom+xml";
        /// <summary>
        /// JSON格式
        /// </summary>
        public const string JsonType = "application/json";

        /// <summary>
        /// PDF格式
        /// </summary>
        public const string PdfType = "application/pdf";
        /// <summary>
        /// WORD格式
        /// </summary>
        public const string WordType = " application/msword";
        /// <summary>
        /// 二进制数据格式
        /// </summary>
        public const string StreamType = "application/octet-stream";
        /// <summary>
        /// 表单提交数据格式
        /// </summary>
        public const string FromType = "application/x-www-form-urlencoded";
        /// <summary>
        /// 表单中文件上传的格式
        /// </summary>
        public const string MuiltType = "multipart/form-data";
        #endregion


        #region [媒体类型]
        /// <summary>
        /// HTML格式
        /// </summary>
        public const string TextHtmlType = "text/html";
        /// <summary>
        /// 纯文本格式
        /// </summary>
        public const string TextPlainType = " text/plain";
        /// <summary>
        /// XML格式
        /// </summary>
        public const string TextXmlType = "text/xml";
        /// <summary>
        /// GIF格式
        /// </summary>
        public const string TextGifType = "image/gif";
        /// <summary>
        /// jpg图片格式
        /// </summary>
        public const string TextJpegType = "image/jpeg";
        /// <summary>
        /// png图片格式
        /// </summary>
        public const string TextPngType = "image/png";
        #endregion
    }
}
