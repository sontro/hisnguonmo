using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDepaUpdate.ExpMestDepaUpdate
{
    class ExpMestDepaUpdateBehavior : Tool<IDesktopToolContext>, IExpMestDepaUpdate
    {
        object[] entity;
        V_HIS_EXP_MEST expMest = null;

        internal ExpMestDepaUpdateBehavior()
            : base()
        {

        }

        internal ExpMestDepaUpdateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IExpMestDepaUpdate.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }

                        if (entity[i] is V_HIS_EXP_MEST)
                        {
                            expMest = (V_HIS_EXP_MEST)entity[i];
                        }
                    }
                }
                if (moduleData != null && expMest != null)
                {
                    return new frmExpMestDepaUpdate(moduleData, expMest);
                }
                else
                {
                    return null;
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
