using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportMachineIndex.ADO
{
    public class MachineIndexImportADO : LIS_MACHINE_INDEX
    {
        public string MACHINE_CODE { get; set; }
        public string MACHINE_NAME { get; set; }
        public string RESULT_COEFFICIENT_STR { get; set; }
        public string ERROR { get; set; }

        public MachineIndexImportADO()
        {
        }

        public MachineIndexImportADO(LIS_MACHINE_INDEX data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MachineIndexImportADO>(this, data);
        }
    }
}
