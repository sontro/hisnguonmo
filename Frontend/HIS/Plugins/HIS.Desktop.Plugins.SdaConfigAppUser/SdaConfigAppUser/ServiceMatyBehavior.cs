using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.SdaConfigAppUser.SdaConfigAppUser
{
    class SdaConfigAppUserBehavior : Tool<IDesktopToolContext>, ISdaConfigAppUser
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal SdaConfigAppUserBehavior()
            : base()
        {

        }

        internal SdaConfigAppUserBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISdaConfigAppUser.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;

                List<Inventec.Common.WebApiClient.ApiConsumer> listApiConsumer = null;
                List<long> listLong = null;
                List<string> listString = null;
                List<Action> listAction = null;
                List<object> listObj = null;

                if (entity != null && entity.Count() > 0)
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is List<Inventec.Common.WebApiClient.ApiConsumer>)
                            {
                                listApiConsumer = (List<Inventec.Common.WebApiClient.ApiConsumer>)entity[i];
                            }
                            if (entity[i] is List<long>)
                            {
                                listLong = (List<long>)entity[i];
                            }
                            if (entity[i] is List<Action>)
                            {
                                listAction = (List<Action>)entity[i];
                            }
                            if (entity[i] is List<string>)
                            {
                                listString = (List<string>)entity[i];
                            }
                            if (entity[i] is List<object>)
                            {
                                listObj = (List<object>)entity[i];
                            }
                        }
                    }
                }

                //Xử lý dữ liệu đầu vào

                result = new UCSdaConfigAppUser(moduleData, listApiConsumer[0], listApiConsumer[1], listAction[0], listLong[0], listString[0]);
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
