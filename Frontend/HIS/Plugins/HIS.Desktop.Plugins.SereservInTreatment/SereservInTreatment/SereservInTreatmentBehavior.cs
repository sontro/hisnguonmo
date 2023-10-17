using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SereservInTreatment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.SereservInTreatment.SereservInTreatment
{
    public sealed class SereservInTreatmentBehavior : Tool<IDesktopToolContext>, ISereservInTreatment
    {
        object[] entity;
        public SereservInTreatmentBehavior()
            : base()
        {
        }

        public SereservInTreatmentBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISereservInTreatment.Run()
        {
            try
            {
                SereservInTreatmentADO sereservInTreatmentADO = null;
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is SereservInTreatmentADO)
                        {
                            sereservInTreatmentADO = (SereservInTreatmentADO)entity[i];
                        }
                    }
                }

                return new frmSereservInTreatment(moduleData, sereservInTreatmentADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
