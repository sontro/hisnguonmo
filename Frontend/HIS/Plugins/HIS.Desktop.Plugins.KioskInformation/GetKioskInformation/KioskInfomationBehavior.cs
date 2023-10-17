using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.Plugins.KioskInformation.GetKioskInformation
{
    class KioskInfomationBehavior : BusinessBase, IKioskInformation //  using Inventec.Core; BusinessBase : EntityBase
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;
        internal KioskInfomationBehavior()
            : base()
        {
        }
        public KioskInfomationBehavior(CommonParam param, object[] data)
            : base()
        {
            this.entity = data;
        }
        object IKioskInformation.Run()
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
                        result = new frmGreetingScreen(Module);
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
