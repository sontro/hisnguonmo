using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportHisCashierAddCfg.ADO
{
    public class CashierAddCfgImportADO : HIS_CASHIER_ADD_CONFIG
    {

        public string REQUEST_ROOM_CODE_STR { get; set; }
        public string REQUEST_ROOM_NAME_STR { get; set; }

        public string EXECUTE_ROOM_CODE_STR { get; set; }
        public string EXECUTE_ROOM_NAME_STR { get; set; }

        public string CASHIER_ROOM_CODE_STR { get; set; }
        public string CASHIER_ROOM_NAME_STR { get; set; }

        public string INSTR_DAY_FROM_STR { get; set; }
        public string INSTR_DAY_TO_STR { get; set; }

        public string INSTR_TIME_FROM_STR { get; set; }
        public string INSTR_TIME_TO_STR { get; set; }

        public string IS_NOT_PRIORITY_STR { get; set; }

        public string ERROR { get; set; }

        public CashierAddCfgImportADO()
        {
        }

        public CashierAddCfgImportADO(HIS_CASHIER_ADD_CONFIG data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<CashierAddCfgImportADO>(this, data);
        }
    }
}
