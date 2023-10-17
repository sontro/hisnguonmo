using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    class PacsReceivedData
    {
        public long SereServId { get; set; }
        public long? BeginTime { get; set; }
        public long? EndTime { get; set; }
        public long? FinishTime { get; set; }
        public string Description { get; set; }
        public string SubclinicalResultLoginname { get; set; }
        public string SubclinicalResultUsername { get; set; }
        public string Machine { get; set; }
        public string Conclude { get; set; }
        public string Note { get; set; }
        public string studyID { get; set; }
        public long? NumberOfFilm { get; set; }
    }
}
