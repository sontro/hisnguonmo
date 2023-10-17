using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStoreV2.ADO
{
    public class TreatmentADO : L_HIS_TREATMENT_3
    {
        public bool CheckTreatment { get; set; }
        public bool CheckStore { get; set; }

        public TreatmentADO() { }

        public TreatmentADO(L_HIS_TREATMENT_3 data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<TreatmentADO>(this, data);
                //this.CheckTreatment = data.DATA_STORE_ID != null ? true : false;
            }
        }
    }
}
