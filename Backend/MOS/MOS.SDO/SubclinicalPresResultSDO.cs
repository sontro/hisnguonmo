using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class SubclinicalPresResultSDO
    {
        public List<HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<HIS_EXP_MEST> ExpMests { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> Medicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> Materials { get; set; }
        public HIS_SERE_SERV_EXT SereServExt { get; set; }
    }
}
