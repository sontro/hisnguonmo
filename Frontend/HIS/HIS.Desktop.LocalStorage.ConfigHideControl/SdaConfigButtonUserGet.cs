using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using SDA.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.ConfigButton
{
    class SdaConfigButtonUserGet
    {
        internal static List<SDA.EFMODEL.DataModels.SDA_MODULE_BUTTON_USER> Get(List<long> moduleButtonIds)
        {
            try
            {
                CommonParam param = new CommonParam();
                string loginName = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
                SdaConfigAppUserFilter filter = new SdaConfigAppUserFilter();
                filter.IS_ACTIVE = 1;
                filter.LOGINNAME = loginName;
                //if (moduleButtonIds != null && moduleButtonIds.Count > 0)
                //    filter.MODULE_BUTTON_IDs = moduleButtonIds;
                return new BackendAdapter(param).Get<List<SDA.EFMODEL.DataModels.SDA_MODULE_BUTTON_USER>>("api/SdaModuleButtonUser/Get", ApiConsumers.SdaConsumer, filter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
