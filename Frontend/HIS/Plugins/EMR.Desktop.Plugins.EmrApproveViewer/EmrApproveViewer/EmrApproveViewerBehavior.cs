using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrApproveViewer.EmrApproveViewer
{
    class EmrApproveViewerBehavior : Tool<IDesktopToolContext>, IEmrApproveViewer
    {
        object[] entity;

        internal EmrApproveViewerBehavior()
            : base()
        { }

        internal EmrApproveViewerBehavior(Inventec.Core.CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IEmrApproveViewer.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            HIS.Desktop.Common.DelegateRefreshData dlg = null;
            long viewerId = 0;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is long)
                        {
                            viewerId = (long)item;
                        }
                        if (item is HIS.Desktop.Common.DelegateRefreshData)
                        {
                            dlg = (HIS.Desktop.Common.DelegateRefreshData)item;
                        }
                    }
                }

                if (moduleData != null && viewerId > 0 && dlg != null)
                {
                    Inventec.Common.Logging.LogSystem.Error("Input:" + viewerId.ToString());
                    return new frmEmrApproveViewer(moduleData, viewerId, dlg);
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
