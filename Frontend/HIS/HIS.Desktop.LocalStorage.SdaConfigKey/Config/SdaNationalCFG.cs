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
    public class SdaNationalCFG
    {
        private const string NATIONAL_CODE_BASE = "EXE.NATIONAL_CODE_BASE";

        private static SDA_NATIONAL nationalBase;
        public static SDA_NATIONAL NATIONAL_BASE
        {
            get
            {
                if (nationalBase == null)
                {
                    nationalBase = GetData(NATIONAL_CODE_BASE);
                }
                return nationalBase;
            }
            set
            {
                nationalBase = value;
            }
        }

        private static SDA.EFMODEL.DataModels.SDA_NATIONAL GetData(string code)
        {
            SDA_NATIONAL result = null;
            try
            {
                SDA.EFMODEL.DataModels.SDA_CONFIG config = Inventec.Common.LocalStorage.SdaConfig.ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(code);
                SdaNationalFilter filter = new SdaNationalFilter();
                filter.KEY_WORD = value;
                var data = BackendDataWorker.Get<SDA_NATIONAL>().FirstOrDefault(o => o.NATIONAL_CODE == value);
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
