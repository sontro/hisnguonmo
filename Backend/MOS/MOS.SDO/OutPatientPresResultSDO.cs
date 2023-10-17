using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class OutPatientPresResultSDO
    {
        public List<HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<HIS_EXP_MEST> ExpMests { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> Medicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> Materials { get; set; }
        public List<HIS_SERVICE_REQ_MATY> ServiceReqMaties { get; set; }
        public List<HIS_SERVICE_REQ_METY> ServiceReqMeties { get; set; }
        public HIS_MEDI_RECORD MediRecord { get; set; }
        public List<HIS_SERE_SERV> SereServs { get; set; }
    }
}
