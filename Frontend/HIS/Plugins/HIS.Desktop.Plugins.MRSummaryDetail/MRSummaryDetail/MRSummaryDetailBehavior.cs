using HIS.Desktop.ADO;
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

namespace HIS.Desktop.Plugins.MRSummaryDetail.MRSummaryDetail
{
    class MRSummaryDetailBehavior : Tool<IDesktopToolContext>, IMRSummaryDetail
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        MRSummaryDetailADO ado = null;
        RefeshReference delegateRefresh = null;
        internal MRSummaryDetailBehavior()
            : base()
        {

        }

        internal MRSummaryDetailBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMRSummaryDetail.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is MRSummaryDetailADO)
                            ado = (MRSummaryDetailADO)item;
                        if (item is RefeshReference)
                            delegateRefresh = (RefeshReference)item;
                    }
                }

                if (moduleData != null)
                {
                    return new HIS.Desktop.Plugins.MRSummaryDetail.Run.frmMRSummaryDetail(moduleData, ado, delegateRefresh);
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
