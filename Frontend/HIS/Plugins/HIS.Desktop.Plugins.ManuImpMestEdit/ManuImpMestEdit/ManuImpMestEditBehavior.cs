using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.ManuImpMestEdit.ManuImpMestEdit
{
    class ManuImpMestEditBehavior : Tool<IDesktopToolContext>, IManuImpMestEdit
    {
        object[] entity;

        internal ManuImpMestEditBehavior()
            : base()
        {

        }

        internal ManuImpMestEditBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IManuImpMestEdit.Run()
        {
            long result = 0;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is MOS.EFMODEL.DataModels.V_HIS_IMP_MEST)
                            result = ((MOS.EFMODEL.DataModels.V_HIS_IMP_MEST)item).ID;
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new FormManuImpMestEdit(result, moduleData);
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
