using ACS.EFMODEL.DataModels;
using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SdaConfigApp.SdaConfigApp
{
    class SdaConfigAppBehavior : BusinessBase, ISdaConfigApp
    {
        object[] entity;
        internal SdaConfigAppBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISdaConfigApp.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                Inventec.Common.WebApiClient.ApiConsumer SdaConsumer = null;
                long configNumPageSize = 0;
                List<string> listString = null;
                string applicationCode = "";
                Action delegateRefresh = null;
                string iconPath = "";
                List<ACS_APPLICATION> listAcsApplication = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is Inventec.Common.WebApiClient.ApiConsumer)
                            {
                                SdaConsumer = (Inventec.Common.WebApiClient.ApiConsumer)entity[i];
                            }
                            if (entity[i] is long)
                            {
                                configNumPageSize = (long)entity[i];
                            }
                            if (entity[i] is Action)
                            {
                                delegateRefresh = (Action)entity[i];
                            }
                            if (entity[i] is List<string>)
                            {
                                listString = (List<string>)entity[i];
                            }
                            if (entity[i] is List<ACS_APPLICATION>)
                            {
                                listAcsApplication = (List<ACS_APPLICATION>)entity[i];
                            }
                        }
                    }
                }

                if (listString.Count >= 2)
                {
                    applicationCode = listString[0];
                    iconPath = listString[1];
                }

                result = new frmSdaConfigApp(moduleData, SdaConsumer, delegateRefresh, configNumPageSize, applicationCode, iconPath, listAcsApplication);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }
    }
}
