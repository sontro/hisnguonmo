using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ListSurgMisuByTreatment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ListSurgMisuByTreatment.ListSurgMisuByTreatment
{
    public sealed class ListSurgMisuByTreatmentBehavior : Tool<IDesktopToolContext>, IListSurgMisuByTreatment
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId;
        long patientTypeId;

        public ListSurgMisuByTreatmentBehavior()
            : base()
        {
        }

        public ListSurgMisuByTreatmentBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IListSurgMisuByTreatment.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is long)
                        {
                            treatmentId = (long)item;
                        }
                        else if (item is string)
                        {
                            patientTypeId = Convert.ToInt64(item);
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        if (currentModule != null && treatmentId > 0 && patientTypeId > 0)
                        {
                            result = new HIS.Desktop.Plugins.ListSurgMisuByTreatment.Run.frmListSurgMisuByTreatment(currentModule, treatmentId,patientTypeId);
                            break;
                        }
                        else if (currentModule != null && treatmentId > 0)
                        {
                            result = new HIS.Desktop.Plugins.ListSurgMisuByTreatment.Run.frmListSurgMisuByTreatment(currentModule, treatmentId);
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
