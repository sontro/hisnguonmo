using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.AnticipateCreate.AnticipateCreate
{
    class AnticipateCreateBehavior : Tool<IDesktopToolContext>, IAnticipateCreate
    {
        object[] entity;
        internal AnticipateCreateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAnticipateCreate.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_ANTICIPATE anticipate = null;
                HIS.Desktop.Common.DelegateRefreshData delegateRefresh = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is V_HIS_ANTICIPATE)
                        {
                            anticipate = (V_HIS_ANTICIPATE)entity[i];
                        }
                        if (entity[i] is HIS.Desktop.Common.DelegateRefreshData)
                        {
                            delegateRefresh = (HIS.Desktop.Common.DelegateRefreshData)entity[i];
                        }
                    }
                }
                if (moduleData != null && anticipate != null && delegateRefresh!=null)
                {
                    return new frmAnticipateUpdate(moduleData, anticipate, delegateRefresh);
                }
                else
                {
                    return new UCAnticipateCreate(moduleData);
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
