
using Inventec.Common.Logging;
using Newtonsoft.Json;
using System;
using MOS.LibraryHein.Common;

namespace MOS.LibraryHein.Ksk
{
    public class KskServiceRequestData : RequestServiceData
    {
        public KskPatientTypeData PatientTypeData { get; set; }

        public KskServiceRequestData(RequestServiceData data)
            : base(data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.JsonPatientTypeData))
                {
                    throw new Exception("data.JsonPatientTypeData ko co du lieu");
                }
                this.PatientTypeData = JsonConvert.DeserializeObject<KskPatientTypeData>(data.JsonPatientTypeData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                throw ex;
            }
        }

        public KskServiceRequestData(string jsonPatientTypeData)
            : base(jsonPatientTypeData)
        {
            try
            {
                this.PatientTypeData = JsonConvert.DeserializeObject<KskPatientTypeData>(jsonPatientTypeData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                throw ex;
            }
        }

        public KskServiceRequestData(KskPatientTypeData patientTypeData)
        {
            try
            {
                this.PatientTypeData = patientTypeData;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                throw ex;
            }
        }
    }
}
