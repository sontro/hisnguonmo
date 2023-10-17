using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Utility;
using SDA.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ModuleControlVisible.ChooseRoom
{
    class ChooseRoomBehavior : BusinessBase, IChooseRoom
    {
        object[] entity;
        internal ChooseRoomBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IChooseRoom.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                DelegateSelectData refeshReference = null;
                SDA_HIDE_CONTROL hideControl = null;
                List<ModuleControlADO> moduleControlADOs = null;
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                    if (item is DelegateSelectData)
                    {
                        refeshReference = (DelegateSelectData)item;
                    }
                    if (item is List<ModuleControlADO>)
                    {
                        moduleControlADOs = (List<ModuleControlADO>)item;
                    }
                    if (item is SDA_HIDE_CONTROL)
                    {
                        hideControl = (SDA_HIDE_CONTROL)item;
                    }
                }
                return new frmModuleControlVisible(module, refeshReference, moduleControlADOs, hideControl);
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
