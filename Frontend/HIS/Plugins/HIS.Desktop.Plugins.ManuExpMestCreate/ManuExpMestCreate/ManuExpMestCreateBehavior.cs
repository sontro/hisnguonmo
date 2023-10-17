using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ManuExpMestCreate;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.ManuExpMestCreate.ManuExpMestCreate
{
    public sealed class ManuExpMestCreateBehavior : Tool<IDesktopToolContext>, IManuExpMestCreate
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        MOS.EFMODEL.DataModels.V_HIS_IMP_MEST _ManuImpMest;
        MOS.EFMODEL.DataModels.V_HIS_EXP_MEST _ManuExpMest;

        public ManuExpMestCreateBehavior()
            : base()
        {
        }

        public ManuExpMestCreateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IManuExpMestCreate.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is MOS.EFMODEL.DataModels.V_HIS_IMP_MEST)
                        {
                            _ManuImpMest = (MOS.EFMODEL.DataModels.V_HIS_IMP_MEST)item;
                        }
                        else if (item is MOS.EFMODEL.DataModels.V_HIS_EXP_MEST)
                        {
                            _ManuExpMest = (MOS.EFMODEL.DataModels.V_HIS_EXP_MEST)item;
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }
                    if (currentModule != null && _ManuImpMest != null)
                    {
                        result = new HIS.Desktop.Plugins.ManuExpMestCreate.frmManuExpMestCreate(currentModule, _ManuImpMest);
                    }
                    else if (currentModule != null && _ManuExpMest != null)
                    {
                        result = new HIS.Desktop.Plugins.ManuExpMestCreate.frmManuExpMestCreate(currentModule, _ManuExpMest);
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
