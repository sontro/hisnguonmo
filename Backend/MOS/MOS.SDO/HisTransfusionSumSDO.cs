using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransfusionSumSDO
    {
        public string Puc { get; set; }
        public long ExpMestBloodId { get; set; }
        public long TreatmentId { get; set; }
        public long? RequestRoomId { get; set; }
        public long? StartTime { get; set; }
        public long? FinishTime { get; set; }
        public long? NumOrder { get; set; }
        public long? TransfusionVolume { get; set; }
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public string Note { get; set; }
    }
}
