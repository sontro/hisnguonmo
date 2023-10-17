using Inventec.Common.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.OtherHein
{
    public class OtherPatientTypeData
    {
        public long ID { get; set; }
        public Nullable<long> CREATE_TIME { get; set; }
        public Nullable<long> MODIFY_TIME { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER { get; set; }
        public string APP_CREATOR { get; set; }
        public string APP_MODIFIER { get; set; }
        public Nullable<short> IS_ACTIVE { get; set; }
        public Nullable<short> IS_DELETE { get; set; }
        public string GROUP_CODE { get; set; }
        public long PATIENT_TYPE_ALTER_ID { get; set; }
        public string POLICY_NUMBER { get; set; }
        public Nullable<long> FROM_TIME { get; set; }
        public Nullable<long> TO_TIME { get; set; }
        public long TREATMENT_LOG_ID { get; set; }
        public long PATIENT_TYPE_ID { get; set; }
        public Nullable<long> TDL_PATIENT_ID { get; set; }
        public long TREATMENT_TYPE_ID { get; set; }
        public string PATIENT_CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string VIR_PATIENT_NAME { get; set; }
        public string VIR_ADDRESS { get; set; }
        public Nullable<long> GENDER_ID { get; set; }
        public Nullable<long> DOB { get; set; }
        public string GENDER_CODE { get; set; }
        public string GENDER_NAME { get; set; }

        public string ToJsonString()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }

        public static OtherPatientTypeData FromJsonString(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<OtherPatientTypeData>(jsonString);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }
    }
}
