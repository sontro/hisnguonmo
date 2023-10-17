using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStore.ADO
{
    public class TreatmentADO : V_HIS_TREATMENT_9
    {
        public bool CheckTreatment { get; set; }
        public bool CheckStore { get; set; }

        public TreatmentADO() { }

        public TreatmentADO(V_HIS_TREATMENT_9 data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<TreatmentADO>(this, data);
                //this.CheckTreatment = data.DATA_STORE_ID != null ? true : false;
            }
        }
    }
}
