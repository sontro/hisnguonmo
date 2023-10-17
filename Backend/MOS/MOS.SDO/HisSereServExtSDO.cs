using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class FileSDO
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public long? BodyPartId { get; set; }
    }

    public class HisSereServExtSDO
    {
        public HIS_SERE_SERV_EXT HisSereServExt { get; set; }
        public HIS_SERE_SERV_PTTT HisSereServPttt { get; set; }
        public List<HIS_EKIP_USER> HisEkipUsers { get; set; }
        public List<FileSDO> Files { get; set; }
    }
}
