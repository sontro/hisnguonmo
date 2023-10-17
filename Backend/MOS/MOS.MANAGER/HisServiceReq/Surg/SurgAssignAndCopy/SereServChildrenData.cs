using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.SurgAssignAndCopy
{
    public class SereServChildrenData
    {
        public HIS_SERE_SERV_EXT SereServExt { get; set; }
        public HIS_SERE_SERV_PTTT SereServPttt { get; set; }

        public List<HIS_EKIP_USER> EkipUsers { get; set; }
        public List<HIS_SERE_SERV_FILE> SereServFiles { get; set; }
        public List<HIS_SESE_PTTT_METHOD> SereServPtttMethods { get; set; }
        public List<HIS_STENT_CONCLUDE> StentConcludes { get; set; }

        public HIS_EYE_SURGRY_DESC EyeSurgryDesc { get; set; }  // Thong tin "Mat"
        public HIS_SKIN_SURGERY_DESC SkinSurgeryDesc { get; set; }  // Thong tin "Da lieu"
    }
}
