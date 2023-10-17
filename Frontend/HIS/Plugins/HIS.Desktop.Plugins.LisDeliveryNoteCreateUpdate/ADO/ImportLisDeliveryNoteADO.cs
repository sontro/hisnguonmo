using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteCreateUpdate.ADO
{
    public class ImportLisDeliveryNoteADO : V_LIS_SAMPLE
    {
        public string MESSAGE_ERR { get; set; }
        public string MESSAGE_WARN { get; set; }
        public string BARCODE_TMP { get; set; }
        public long? DOB_TMP { get; set; }
        public string VIR_PATIENT_NAME_TMP { get; set; }
        public string GENDER_NAME_TMP { get; set; }
        public string GENDER_CODE_TMP { get; set; }
        public string PHONE_NUMBER_TMP { get; set; }
        public string SAMPLE_TYPE_CODE_TMP { get; set; }
        public string SAMPLE_TYPE_NAME_TMP { get; set; }
        public string SAMPLE_LOGINNAME_TMP { get; set; }
        public string COMMUNE_NAME_TMP { get; set; }
        public string DISTRICT_NAME_TMP { get; set; }
        public string PROVINCE_NAME_TMP { get; set; }
        public string CMND_NUMBER_TMP { get; set; }
        public string HEIN_CARD_NUMBER_TMP { get; set; }
        public string NATIONAL_NAME_TMP { get; set; }
        public string PASSPORT_NUMBER_TMP { get; set; }
        public string VIR_ADDRESS_TMP { get; set; }
        public decimal? SICK_TIME_TMP { get; set; }
        public string DAY_SAMPLE_TIME_TMP { get; set; }
        public string HOURS_SAMPLE_TIME_TMP { get; set; }
        public long? SAMPLE_TIME_TMP { get; set; }
        public long? SPECIMEN_ORDER_TMP { get; set; }

        public ImportLisDeliveryNoteADO()
        {

        }
        public ImportLisDeliveryNoteADO(V_LIS_SAMPLE data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<V_LIS_SAMPLE>(this, data);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
