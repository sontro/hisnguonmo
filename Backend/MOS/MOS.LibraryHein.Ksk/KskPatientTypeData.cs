using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Ksk
{
    public class KskPatientTypeData : HIS_PATY_ALTER_KSK
    {
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

        public static KskPatientTypeData FromJsonString(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<KskPatientTypeData>(jsonString);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }
    }
}
