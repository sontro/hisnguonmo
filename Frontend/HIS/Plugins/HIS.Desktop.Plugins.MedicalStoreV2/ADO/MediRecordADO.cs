using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStoreV2.ADO
{
    public class MediRecordADO : V_HIS_MEDI_RECORD_1
    {
        public bool CheckTreatment { get; set; }
        public bool CheckStore { get; set; }

        public MediRecordADO() { }

        public MediRecordADO(V_HIS_MEDI_RECORD_1 data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MediRecordADO>(this, data);
                //this.CheckTreatment = data.DATA_STORE_ID != null ? true : false;
            }
        }
    }
}
