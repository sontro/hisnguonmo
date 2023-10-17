using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceUnitEdit
{
    public class ServiceUnitADO : HIS_SERVICE_UNIT
    {
        public int Action { get; set; }
        public long? ID_GRID { get; set; }
        public ServiceUnitADO(int actionType, HIS_SERVICE_UNIT data)
        {
            this.Action = actionType;
            Inventec.Common.Mapper.DataObjectMapper.Map<ServiceUnitADO>(this, data);

        }
        public ServiceUnitADO(HIS_SERVICE_UNIT data)
            : this(HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionView, data)
        {
        }
    }
}
