using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodTransfusion.ADO
{
    class TransfusionADO : HIS_TRANSFUSION
    {
        public DateTime MEASURE_TIME_STR { get; set; }

        public TransfusionADO()
        {
        }

        public TransfusionADO(HIS_TRANSFUSION data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<TransfusionADO>(this, data);
            this.MEASURE_TIME_STR = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.MEASURE_TIME) ?? new DateTime();
        }
    }
}
