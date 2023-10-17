using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentForRecordCheckingSDO
    {
        public HIS_TREATMENT Treatment { get; set; }
        public List<HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<HIS_TRACKING> Trackings { get; set; }
        public List<HIS_INFUSION> Infusions { get; set; }
        public List<HIS_CARE> Cares { get; set; }
        public List<HIS_MEDI_REACT> MediReacts { get; set; }
        public List<HIS_DEBATE> Debates { get; set; }
        public List<HIS_TRANSFUSION> Transfusions { get; set; }
    }
}
