using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TrackImmunizationHistoryInfor;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.TrackImmunizationHistoryInfor.TrackImmunizationHistoryInfor
{
    public sealed class TrackImmunizationHistoryInforBehavior : Tool<IDesktopToolContext>, ITrackImmunizationHistoryInfor
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId;
        public TrackImmunizationHistoryInforBehavior()
            : base()
        {
        }

        public TrackImmunizationHistoryInforBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITrackImmunizationHistoryInfor.Run()
        {
            object result = null;
            try
            {
                HIS.Desktop.Common.DelegateRefreshData _dlgRef = null;
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
                            treatmentId = (long)item;
                        }
                        else if (item is DelegateRefreshData)
                        {
                            _dlgRef = (DelegateRefreshData)item;
                        }
                    }
                    if (currentModule != null)
                    {
                        result = new frmTrackImmunizationHistoryInfor(currentModule);
                        if (_dlgRef != null)
                        {
                            // result = new frmTrackImmunizationHistoryInfor(currentModule, treatmentId, _dlgRef);
                        }
                        else
                        {
                            // result = new frmTrackImmunizationHistoryInfor(currentModule, treatmentId);
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
