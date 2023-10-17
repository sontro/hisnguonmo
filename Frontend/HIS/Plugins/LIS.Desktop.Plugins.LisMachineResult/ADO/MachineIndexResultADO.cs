using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisMachineResult.ADO
{
    public class MachineIndexResultADO : V_LIS_MACHINE_INDEX_RESULT
    {
        public string TEST_INDEX_CODE { get; set; }
        public string TEST_INDEX_NAME { get; set; }
        public MachineIndexResultADO() { }
        public MachineIndexResultADO(V_LIS_MACHINE_INDEX_RESULT data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MachineIndexResultADO>(this, data);
        }
    }
}
