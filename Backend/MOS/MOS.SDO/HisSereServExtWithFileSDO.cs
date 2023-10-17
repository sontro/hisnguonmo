using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisSereServExtWithFileSDO
    {
        public HIS_SERE_SERV_EXT SereServExt { get; set; }
        public HIS_SERE_SERV_PTTT SereServPttt { get; set; }
        public List<HIS_SERE_SERV_FILE> SereServFiles { get; set; }
        public List<HIS_EKIP_USER> EkipUsers { get; set; }
    }
}
