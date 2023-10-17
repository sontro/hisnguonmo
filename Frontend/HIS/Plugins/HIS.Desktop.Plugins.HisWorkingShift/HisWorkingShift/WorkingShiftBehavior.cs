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

namespace HIS.Desktop.Plugins.HisWorkingShift.HisWorkingShift
{
    class WorkingShiftBehavior : BusinessBase, IWorkingShift
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        internal WorkingShiftBehavior()
            : base()
        {

        }

        internal WorkingShiftBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IWorkingShift.Run()
        {

            try
            {
                RefeshReference refeshReference = null;
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                    if (item is RefeshReference)
                    {
                        refeshReference = (RefeshReference)item;
                    }
                }
                return new frmWorkingShift(currentModule, refeshReference);
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
