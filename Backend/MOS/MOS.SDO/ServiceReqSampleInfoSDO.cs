using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ServiceReqSampleInfoSDO
    {
        public long ServiceReqId { get; set; }
        public long ReqRoomId { get; set; }
        public long? SampleTime { get; set; }
        public string SamplerLoginname { get; set; }
        public string SamplerUsername { get; set; }
        public long? TestSampleTypeId { get; set; }
        public bool IsCancel { get; set; }
    }
}
