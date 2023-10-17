using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisSereServWithFileSDO
    {
        public HIS_SERE_SERV HisSereServ { get; set; }
        public List<HIS_SERE_SERV_FILE> HisSereServFiles { get; set; }
    }
}
