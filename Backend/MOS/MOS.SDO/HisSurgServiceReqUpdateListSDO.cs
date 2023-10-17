using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class SurgUpdateSDO
    {
        public long SereServId { get; set; }
        public HIS_SERE_SERV_PTTT SereServPttt { get; set; }
        public HIS_SERE_SERV_EXT SereServExt { get; set; }
        public HIS_EYE_SURGRY_DESC EyeSurgryDesc { get; set; }
        public HIS_SKIN_SURGERY_DESC SkinSurgeryDesc { get; set; }
        public List<HIS_EKIP_USER> EkipUsers { get; set; }
        public List<HisSesePtttMethodSDO> SesePtttMethos { get; set; }
        public List<HIS_STENT_CONCLUDE> StentConcludes { get; set; }
    }

    public class HisSurgServiceReqUpdateListSDO
    {
        public bool UpdateInstructionTimeByStartTime { get; set; }
        public bool IsFinished { get; set; }
        public List<SurgUpdateSDO> SurgUpdateSDOs { get; set; }
    }
}
