using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BrowseExportTicket;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;

namespace Inventec.Desktop.Plugins.BrowseExportTicket.BrowseExportTicket
{
    public sealed class BrowseExportTicketBehavior : Tool<IDesktopToolContext>, IBrowseExportTicket
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long expMestId = 0;
        public BrowseExportTicketBehavior()
            : base()
        {
        }

        public BrowseExportTicketBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IBrowseExportTicket.Run()
        {
            object result = null;
            DelegateSelectData delegateSelectData = null;
            MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_4 expMest = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            expMestId = (long)item;
                        }
                        else if (item is DelegateSelectData)
                        {
                            delegateSelectData = (DelegateSelectData)item;
                        }
                        else if (item is MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_4)
                        {
                            expMest = (MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_4)item;
                        }
                    }
                    if (currentModule != null && expMestId > 0)
                    {
                        result = new frmBrowseExportTicket(currentModule, expMestId, delegateSelectData);
                    }
                    else if (currentModule != null && expMest != null)
                    {
                        result = new frmBrowseExportTicket(currentModule, expMest, delegateSelectData);
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
