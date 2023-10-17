using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStoreV2.ADO
{
    public class DataStoreADO : V_HIS_DATA_STORE_1
    {
        public bool CheckStore { get; set; }
        public string DataStoreNameWithCountTreatment { get; set; }
        public bool IsChuaLuu { get; set; }

        public DataStoreADO()
        {
        }

        public DataStoreADO(V_HIS_DATA_STORE_1 data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<DataStoreADO>(this, data);
                this.DataStoreNameWithCountTreatment = this.DATA_STORE_NAME + " (" + string.Format("{0:0.####}", this.MEDI_RECORD_COUNT) + ")";
            }
        }
    }
}
