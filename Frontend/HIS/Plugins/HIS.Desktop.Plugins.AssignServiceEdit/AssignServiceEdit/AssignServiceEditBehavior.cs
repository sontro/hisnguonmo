using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceEdit.AssignServiceEdit
{
    class AssignServiceEditBehavior : Tool<IDesktopToolContext>, IAssignServiceEdit
    {
        object[] entity;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        HIS.Desktop.Common.RefeshReference _RefeshReference;
        long serviceReqId = 0;
        long instructionTime = 0;


        internal AssignServiceEditBehavior()
            : base()
        {

        }

        internal AssignServiceEditBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IAssignServiceEdit.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        else if (item is AssignServiceEditADO)
                        {
                            this._RefeshReference = ((AssignServiceEditADO)item).DelegateRefeshReference;
                            this.serviceReqId = ((AssignServiceEditADO)item).serviceReqId;
                            this.instructionTime = ((AssignServiceEditADO)item).instructionTime;
                        }
                    }
                }
                if (moduleData != null && this.serviceReqId > 0)
                {
                    return new FormAssignServiceEdit(serviceReqId, instructionTime, _RefeshReference, moduleData);
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
