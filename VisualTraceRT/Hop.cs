using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualTraceRT
{
    /// <summary>
    /// 单跳数据
    /// </summary>
    public class Hop
    {
        /// <summary>
        /// 跳数
        /// </summary>
        public string Distance { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 地理信息
        /// </summary>
        public string Geo { get; set; }
        /// <summary>
        /// 自治系统
        /// </summary>
        public string AS { get; set; }
        /// <summary>
        /// ISP信息
        /// </summary>
        public string ISP { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// 时延
        /// </summary>
        public string Delay { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Lon { get; set; }
    }
}
