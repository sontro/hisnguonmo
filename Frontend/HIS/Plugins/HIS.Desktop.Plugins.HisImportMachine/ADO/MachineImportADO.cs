using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportMachine.ADO
{
    public class MachineImportADO : LIS_MACHINE
    {
        public string ERROR { get; set; }

        public MachineImportADO()
        {
        }

        public MachineImportADO(LIS_MACHINE data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MachineImportADO>(this, data);
        }
    }
}
