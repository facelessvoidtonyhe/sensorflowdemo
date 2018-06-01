using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorflowManager
{
    public class SensorFlowCommond
    {
        public string monitoringUnit { set; get; }
        public string sampleUnit { set; get; }
        public string channel { set; get; }
        public parameters parameters { set; get; }
        public string phase { set; get; }
        public int timeout { set; get; }
        public string operator1 {set;get;}
        public string startTime { set; get; }
    }

    public class parameters
    {
        public int r { set; get; }
        public int g { set; get; }
        public int b { set; get; }
    }
}
