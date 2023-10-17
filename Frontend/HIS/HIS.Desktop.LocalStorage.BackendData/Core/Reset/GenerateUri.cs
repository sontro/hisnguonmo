using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Core.Reset
{
    class GenerateUri
    {
        internal static string GetTruncateUriByType(string dataKey)
        {
            string uri = "";
            try
            {
                if (dataKey == typeof(MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__MEDICINE_TYPE;
                }
                else if (dataKey == typeof(MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__MATERIAL_TYPE;
                }
                else if (dataKey == typeof(MOS.EFMODEL.DataModels.HIS_ICD).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__ICD;
                }
                else if (dataKey == typeof(MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__SERVICE_PATY;
                }
                else if (dataKey == typeof(MOS.EFMODEL.DataModels.V_HIS_SERVICE).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__SERVICE;
                }
                else if (dataKey == typeof(MOS.EFMODEL.DataModels.V_HIS_SERVICE_ROOM).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__SERVICE_ROOM;
                }
                else if (dataKey == typeof(MOS.EFMODEL.DataModels.HIS_MEDI_ORG).ToString())
                {
                    uri = RDCACHE.URI.RdCache.POST_TRUNCATE__MEDI_ORG;
                }
                else if (dataKey == typeof(SDA.EFMODEL.DataModels.V_SDA_PROVINCE).ToString())
                {
                    uri = "api/RdCache/TruncateProvince";
                }
                else if (dataKey == typeof(SDA.EFMODEL.DataModels.V_SDA_DISTRICT).ToString())
                {
                    uri = "api/RdCache/TruncateDistrict";
                }
                else if (dataKey == typeof(SDA.EFMODEL.DataModels.V_SDA_COMMUNE).ToString())
                {
                    uri = "api/RdCache/TruncateCommune";
                }
                else if (dataKey == typeof(SDA.EFMODEL.DataModels.SDA_NATIONAL).ToString())
                {
                    uri = "api/RdCache/TruncateNational";
                }
                else if (dataKey == typeof(SDA.EFMODEL.DataModels.SDA_ETHNIC).ToString())
                {
                    uri = "api/RdCache/TruncateEthnic";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return uri;
        }
    }
}
