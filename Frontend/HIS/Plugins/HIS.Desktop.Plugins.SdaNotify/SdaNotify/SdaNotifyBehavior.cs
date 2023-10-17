using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SdaNotify
{
    class SdaNotifyBehavior : BusinessBase, ISdaNotify
    {
        object[] entity;
        internal SdaNotifyBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISdaNotify.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                Inventec.Common.WebApiClient.ApiConsumer SdaConsumer = null;
                string iconPath = "";
                long configNumPageSize = 0;

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
                            if (entity[i] is string)
                            {
                                iconPath = (string)entity[i];
                            }
                            if (entity[i] is long)
                            {
                                configNumPageSize = (long)entity[i];
                            }
                        }
                    }
                }

                return new frmSdaNotify(moduleData, SdaConsumer, iconPath, configNumPageSize);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
