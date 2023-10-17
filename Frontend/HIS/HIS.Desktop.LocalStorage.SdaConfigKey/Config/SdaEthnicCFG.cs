using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using SDA.Filter;
using System.Collections.Generic;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class SdaEthnicCFG
    {
        private const string ETHNIC_CODE_BASE = "EXE.ETHNIC_CODE_BASE";

        private static SDA_ETHNIC ethnicBase;
        public static SDA_ETHNIC ETHNIC_BASE
        {
            get
            {
                if (ethnicBase == null)
                {
                    ethnicBase = GetData(ETHNIC_CODE_BASE);
                }
                return ethnicBase;
            }
            set
            {
                ethnicBase = value;
            }
        }

        private static SDA_ETHNIC GetData(string code)
        {
            SDA_ETHNIC result = null;
            try
            {
                SDA.EFMODEL.DataModels.SDA_CONFIG config = Inventec.Common.LocalStorage.SdaConfig.ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(code);
                SdaEthnicFilter filter = new SdaEthnicFilter();
                filter.KEY_WORD = value;
                var data = BackendDataWorker.Get<SDA_ETHNIC>().FirstOrDefault(o => o.ETHNIC_CODE == value);
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => config), config));
                result = data;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
