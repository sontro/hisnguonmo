using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InterconnectionPrescription.ADO
{
    public class DataADO
    {
        public string CODE { get; set; }
        public long? CREATE_TIME { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER { get; set; }
        public long? MODIFY_TIME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NATIONAL { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public long? TDL_PATIENT_DOB { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public long? TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public short? TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long ID { get; set; }
        public long? TDL_INTRUCTION_TIME { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long EXP_MEST_TYPE_ID { get; set; }
        public long IMP_MEST_TYPE_ID { get; set; }
        public long? PRESCRIPTION_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }

        public long? SUPPLIER_ID { get; set; }

        public long? EXP_MEST_REASON_ID { get; set; }

        public DataADO() { }

        public DataADO(V_HIS_EXP_MEST_2 data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<DataADO>(this, data);
                    this.CODE = data.EXP_MEST_CODE;
                    this.NATIONAL = data.NATIONAL_EXP_MEST_CODE;
                    if (this.EXP_MEST_REASON_ID > 0)
                    {
                        var dataRes = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<HIS_EXP_MEST_REASON>().FirstOrDefault(p => p.ID == this.EXP_MEST_REASON_ID);
                        this.DESCRIPTION = dataRes != null ? dataRes.EXP_MEST_REASON_NAME : "";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
