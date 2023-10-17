using MOS.EFMODEL.DataModels;
//using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMachineImport.ADO
{
    class MachineImportADO : HIS_MACHINE
    {
        public string ROOM_CODE { get; set; }
        public long ROOM_TYPE_ID { get; set; }
        //public string ROOM_TYPE_CODE { get; set; }
        public string ERROR { get; set; }
    }
}
