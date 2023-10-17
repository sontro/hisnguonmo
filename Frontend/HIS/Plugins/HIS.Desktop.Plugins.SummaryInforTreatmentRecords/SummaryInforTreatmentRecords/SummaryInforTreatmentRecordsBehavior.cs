using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SummaryInforTreatmentRecords;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.SummaryInforTreatmentRecords.SummaryInforTreatmentRecords
{
    public sealed class SummaryInforTreatmentRecordsBehavior : Tool<IDesktopToolContext>, ISummaryInforTreatmentRecords
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId;
        public SummaryInforTreatmentRecordsBehavior()
            : base()
        {
        }

        public SummaryInforTreatmentRecordsBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISummaryInforTreatmentRecords.Run()
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
                            treatmentId = (long)item;
                        }
                        if (currentModule != null && treatmentId > 0)
                        {
                            result = new frmSummaryInforTreatmentRecords(currentModule, treatmentId);
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
