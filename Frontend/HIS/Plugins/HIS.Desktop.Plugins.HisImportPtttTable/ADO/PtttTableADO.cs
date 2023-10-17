using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportPtttTable.ADO
{
    class PtttTableADO:MOS.EFMODEL.DataModels.HIS_PTTT_TABLE
    {
        public string EXECUTE_ROOM_CODE { get; set; }
        public string ERROR { get; set; }
        public string MAX_CAPACITY_STR { get; set; }
    }
}
