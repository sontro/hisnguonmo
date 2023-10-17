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

namespace HIS.Desktop.LocalStorage.ConfigHideControl
{
    class SdaHideControlGet
    {
        internal static List<SDA.EFMODEL.DataModels.SDA_HIDE_CONTROL> Get()
        {
            try
            {
                CommonParam param = new CommonParam();
                SdaHideControlFilter filter = new SdaHideControlFilter();
                filter.IS_ACTIVE = 1;
                //filter.APP_CODE_ACCEPT = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];
                return new BackendAdapter(param).Get<List<SDA.EFMODEL.DataModels.SDA_HIDE_CONTROL>>("api/SdaHideControl/Get", ApiConsumers.SdaConsumer, filter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
