using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisServiceReqByTracking;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.HisServiceReqByTracking.Run;

namespace HIS.Desktop.Plugins.HisServiceReqByTracking.Run
{
    public sealed class HisServiceReqByTrackingBehavior : Tool<IDesktopToolContext>, IHisServiceReqByTracking
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long trackingId = 0;
        public HisServiceReqByTrackingBehavior()
            : base()
        {
        }

        public HisServiceReqByTrackingBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisServiceReqByTracking.Run()
        {
            object result = null;
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
                            trackingId = (long)item;
                        }
                        if (currentModule != null && trackingId > 0)
                        {
                            result = new frmHisServiceReqByTracking(currentModule, trackingId);
                            break;
                        }
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
