using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.OtherFormAssTreatment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.OtherFormAssTreatment.OtherFormAssTreatment
{
    public sealed class OtherFormAssTreatmentBehavior : Tool<IDesktopToolContext>, IOtherFormAssTreatment
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        OtherFormAssTreatmentInputADO otherFormAssTreatmentInputADO = null;
        public OtherFormAssTreatmentBehavior()
            : base()
        {
        }

        public OtherFormAssTreatmentBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IOtherFormAssTreatment.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    otherFormAssTreatmentInputADO = new OtherFormAssTreatmentInputADO();
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            otherFormAssTreatmentInputADO.TreatmentId = (long)item;
                        }
                        else if (item is OtherFormAssTreatmentInputADO)
                        {
                            otherFormAssTreatmentInputADO = (OtherFormAssTreatmentInputADO)item;
                        }
                    }

                    result = new frmOtherFormAssTreatment(currentModule, otherFormAssTreatmentInputADO);
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
