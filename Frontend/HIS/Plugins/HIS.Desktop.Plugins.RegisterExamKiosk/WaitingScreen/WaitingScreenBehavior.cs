using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.WaitingScreen
{
     class WaitingScreenBehavior : BusinessBase, IWaitingScreen
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;

        internal WaitingScreenBehavior()
            : base()
        {
        }

        public WaitingScreenBehavior(CommonParam param, object[] data)
            : base()
        {
            this.entity = data;
        }

        object IWaitingScreen.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            Module = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    if (Module != null)
                    {
                        result = new frmWaitingScreen(Module);
                        
                    }
                }
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
