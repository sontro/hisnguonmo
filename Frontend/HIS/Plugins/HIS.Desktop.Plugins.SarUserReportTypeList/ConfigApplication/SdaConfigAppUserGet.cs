using Inventec.Common.Adapter;
using Inventec.Core;
using SDA.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SarUserReportTypeList
{
    class SdaConfigAppUserGet
    {
        internal static List<SDA.EFMODEL.DataModels.SDA_CONFIG_APP_USER> Get(List<long> configAppIds)
        {
            try
            {
                CommonParam param = new CommonParam();
                string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                SdaConfigAppUserFilter configAppUserFilter = new SdaConfigAppUserFilter();
                configAppUserFilter.IS_ACTIVE = 1;
                configAppUserFilter.LOGINNAME = loginName;
                if (configAppIds != null && configAppIds.Count > 0)
                    configAppUserFilter.CONFIG_APP_IDs = configAppIds;
                return new BackendAdapter(param).Get<List<SDA.EFMODEL.DataModels.SDA_CONFIG_APP_USER>>("/api/SdaConfigAppUser/Get", ApiConsumers.SdaConsumer, configAppUserFilter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
