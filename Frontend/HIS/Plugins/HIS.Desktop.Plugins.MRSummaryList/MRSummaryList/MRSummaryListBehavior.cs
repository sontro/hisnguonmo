using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MRSummaryList
{
    class MRSummaryListBehavior : Tool<IDesktopToolContext>, IMRSummaryList
    {
        object[] entity;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        MRSummaryDetailADO ado;
        internal MRSummaryListBehavior()
            : base()
        {

        }

        internal MRSummaryListBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMRSummaryList.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        else if (item is MRSummaryDetailADO)
                        {
                            ado = (MRSummaryDetailADO)item;
                        }

                    }
                }
                if (moduleData != null && ado != null)
                {
                    return new frmMRSummaryList(moduleData, ado);
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
