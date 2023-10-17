using Inventec.Core;
using Inventec.Desktop.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vva.Desktop.Plugins.VvaVoiceCommand.VvaVoiceCommand
{
    class VvaVoiceCommandBehavior : BusinessBase, IVvaVoiceCommand
    {
        object[] entity;
        internal VvaVoiceCommandBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IVvaVoiceCommand.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;

                List<Inventec.Common.WebApiClient.ApiConsumer> listApiConsumer = null;
                List<long> listLong = null;
                List<string> listString = null;
                List<Action> listAction = null;
                List<Action<Type>> listActionType = null;

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
                        //if (entity[i] is List<Action<Type>>)
                        //{
                        //    listActionType = (List<Action<Type>>)entity[i];
                        //}
                    }
                }
                result = new frmVvaVoiceCommand(moduleData, listApiConsumer[0], listApiConsumer[1], listAction[0], listLong[0], listString[0], listString[1]);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
            return result;
        }
    }
}
