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

namespace HIS.Desktop.LocalStorage.ConfigCustomizaButton
{
    class SdaCustomizaButtonGet
    {
        internal static List<SDA.EFMODEL.DataModels.SDA_CUSTOMIZE_BUTTON> Get()
        {
            try
            {
                CommonParam param = new CommonParam();
                SdaCustomizeButtonFilter filter = new SdaCustomizeButtonFilter();
                filter.IS_ACTIVE = 1;
                //filter.APP_CODE_ACCEPT = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];
                return new BackendAdapter(param).Get<List<SDA.EFMODEL.DataModels.SDA_CUSTOMIZE_BUTTON>>("api/SdaCustomizeButton/Get", ApiConsumers.SdaConsumer, filter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
