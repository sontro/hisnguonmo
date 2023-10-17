using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedAssign.BedAssign
{
    class BedAssignBehavior: Tool<IDesktopToolContext>, IBedAssign
    {
        object[] entity;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        BedLogADO bedLog;
        internal BedAssignBehavior()
            : base()
        {

        }

        internal BedAssignBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IBedAssign.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        else if (item is BedLogADO)
                        {
                            bedLog = (BedLogADO)item;
                        }
                    }
                }
                if (moduleData != null && bedLog != null)
                {
                    if (bedLog.vHisTreatmentBedRoom != null)
                    {
                        return new FormBedAssign(bedLog.vHisTreatmentBedRoom, moduleData);
                    }
                    else if (bedLog.vHisBedLog != null)
                    {
                        return new FormBedAssign(bedLog.vHisBedLog, moduleData);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
