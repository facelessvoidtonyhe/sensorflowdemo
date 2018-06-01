using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorflowManager
{
    public class SensorFlowAsset
    {
        /// <summary>
        /// 监控单元，一个机柜对应一个监控单元
        /// </summary>
        public string monitoringUnitId { set; get;}
        /// <summary>
        /// U位 tag-2,表示第二U
        /// </summary>
        public string sampleUnitId { set; get; }

        /// <summary>
        /// asset表示资产
        /// </summary>
        public string channelId { set; get; }
        /// <summary>
        /// 当前U所连的资产标签编号
        /// </summary>
        public string value { set; get; }
        public string timestamp { set; get; }
        public string cov { set; get; }
        public int state { set; get; }
    }
}
