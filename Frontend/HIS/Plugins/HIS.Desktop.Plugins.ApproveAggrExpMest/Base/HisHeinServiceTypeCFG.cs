using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.LocalStorage.SdaConfig;
using SDA.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ApproveAggrExpMest.Base
{
    class HisHeinServiceTypeCFG
    {
        private const string HEIN_SERVICE_TYPE_CODE__BLOOD = "MOS.HIS_HEIN_SERVICE_TYPE.HEIN_SERVICE_TYPE_CODE.BLOOD";//mau
        private static long heinServiceTypeId__Blood;
        public static long HEIN_SERVICE_TYPE_ID__BLOOD
        {
            get
            {
                if (heinServiceTypeId__Blood == 0)
                {
                    heinServiceTypeId__Blood = GetId(HEIN_SERVICE_TYPE_CODE__BLOOD);
                }
                return heinServiceTypeId__Blood;
            }
            set
            {
                heinServiceTypeId__Blood = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                SDA_CONFIG config = ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(code);

                var data = GlobalStore.HisHeinServiceTypes.FirstOrDefault(o => o.HEIN_SERVICE_TYPE_CODE == value);
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => config), config));
                result = data.ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }
    }
}
