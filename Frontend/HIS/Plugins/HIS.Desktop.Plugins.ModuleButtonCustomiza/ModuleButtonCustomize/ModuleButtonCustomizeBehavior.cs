using HIS.Desktop.Common;
using HIS.Desktop.Utility;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ModuleButtonCustomize.ModuleButtonCustomize
{
    class ModuleButtonCustomizeBehavior : BusinessBase, IModuleButtonCustomize
    {
        object[] entity;
        internal ModuleButtonCustomizeBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IModuleButtonCustomize.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                RefeshReference refeshReference = null;
                SDA_CUSTOMIZE_BUTTON hideControl = null;
                List<ModuleControlADO> moduleControlADOs = null;
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                    if (item is RefeshReference)
                    {
                        refeshReference = (RefeshReference)item;
                    }
                    if (item is List<ModuleControlADO>)
                    {
                        moduleControlADOs = (List<ModuleControlADO>)item;
                    }
                    if (item is SDA_CUSTOMIZE_BUTTON)
                    {
                        hideControl = (SDA_CUSTOMIZE_BUTTON)item;
                    }
                }
                return new frmModuleButtonCustomize(module, refeshReference, moduleControlADOs, hideControl);
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
