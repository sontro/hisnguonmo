using System.Collections.Generic;

namespace MOS.TDO
{
    public class HisTestResultTDO
    {
        public long? ServiceReqId { get; set; }
        public long? SampleTime { get; set; }
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public string ApproverLoginname { get; set; }
        public string ApproverUsername { get; set; }
        public string SampleLoginname { get; set; }
        public string SampleUsername { get; set; }
        public long? ReceiveSampleTime { get; set; }
        public string ReceiveSampleLoginname { get; set; }
        public string ReceiveSampleUsername { get; set; }
        public bool IsUpdateApprover { get; set; }
        public string ServiceReqCode { get; set; }
        public string AttachmentFileUrl { get; set; }
        public bool? IsCancel { get; set; }
        public List<HisTestIndexResultTDO> TestIndexDatas { get; set; }
        public long? FinishTime { get; set; }
        public string Conclude { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }
}

