using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;

namespace HIS.Desktop.Plugins.HisATCSetUp.ATCSetUp
{
    class ATCSetUpBehavior : Tool<IDesktopToolContext>, IATCSetUp
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        DelegateReturnMutilObject resultAtc;
        List<HIS_ATC> listAtc;
        Module module;
        internal ATCSetUpBehavior()
            : base()
        {

        }

        internal ATCSetUpBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IATCSetUp.Run()
        {
            object result = null;

            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is List<HIS_ATC>)
                        {
                            listAtc = (List<HIS_ATC>)entity[i];
                        }
                        else if (entity[i] is DelegateReturnMutilObject)
                        {
                            resultAtc = (DelegateReturnMutilObject)entity[i];
                        }
                        else if (entity[i] is Module)
                        {
                            module = (Module)entity[i];
                        }
                    }
                    if (listAtc != null && resultAtc != null)
                    {
                        result = new frmATCSetUp(resultAtc, listAtc, module);
                    }
                    else
                    {
                        result = new frmATCSetUp();
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

