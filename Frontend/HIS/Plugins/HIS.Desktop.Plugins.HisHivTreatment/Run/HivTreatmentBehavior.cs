using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisHivTreatment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisHivTreatment.Run
{
    public sealed class HivTreatmentBehavior : Tool<IDesktopToolContext>, IHivTreatment
    {
        object[] entity;
        public HivTreatmentBehavior()
            : base()
        {
        }

        public HivTreatmentBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHivTreatment.Run()
        {
            object result = null;
            DelegateSelectData refeshReference = null;
            HIS_TREATMENT treatment = null;
            Inventec.Desktop.Common.Modules.Module currentModule = null;
            try
            {
                if (entity != null && entity.Length > 0)
                {
                    for (int i = 0; i < entity.Length; i++)
                    {
                        if (entity[i] is HIS_TREATMENT)
                        {
                            treatment = (HIS_TREATMENT)entity[i];
                        }
                        else if (entity[i] is DelegateSelectData)
                        {
                            refeshReference = (DelegateSelectData)entity[i];
                        }
                        else if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                    if (treatment != null)
                    {
                        result = new frmHivTreatment(currentModule, treatment, refeshReference);
                    }
                }
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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
