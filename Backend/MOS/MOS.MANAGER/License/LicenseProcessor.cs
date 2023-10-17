using Inventec.Common.HashUtil;
using Inventec.Core;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.License
{
    public class LicenseProcessor
    {
        private static List<SdaLicenseSDO> data;
        public static List<SdaLicenseSDO> LICENSE_LIST
        {
            get
            {
                return data;
            }
        }

        public static void CheckLicense()
        {
            try
            {
                SdaLicenseFilter filter = new SdaLicenseFilter();
                filter.IS_EXPIRED = false;

                CommonParam param = new CommonParam();
                ApiResultObject<List<SDA_LICENSE>> apiResult = ApiConsumerManager.ApiConsumerStore.SdaConsumer.Get<ApiResultObject<List<SDA_LICENSE>>>("api/SdaLicense/Get", param, filter);
                if (apiResult != null && apiResult.Data != null && apiResult.Data.Count > 0)
                {
                    List<SdaLicenseSDO> licenseList = new List<SdaLicenseSDO>();

                    long dateNow = Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                    if (dateNow == 0)
                    {
                        dateNow = 99999999;
                    }

                    foreach (var license in apiResult.Data)
                    {
                        SdaLicenseSDO sdo = Decrypt(license.LICENSE);
                        if (sdo != null && sdo.ExpiredDate >= dateNow)
                        {
                            licenseList.Add(sdo);
                        }
                    }

                    data = licenseList;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static SdaLicenseSDO Decrypt(string p)
        {
            SdaLicenseSDO result = null;
            try
            {
                if (!String.IsNullOrWhiteSpace(p))
                {
                    string jsonData = new RsaHashProcessor().Decrypt(p);
                    jsonData = jsonData.Substring(jsonData.IndexOf('{'));
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<SdaLicenseSDO>(jsonData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
