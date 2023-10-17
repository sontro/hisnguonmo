using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.TreatmentFinish.TreatmentFinish
{
    class TreatmentFinishBehavior : Tool<IDesktopToolContext>, ITreatmentFinish
    {
        object[] entity;
        internal TreatmentFinishBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITreatmentFinish.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = new Inventec.Desktop.Common.Modules.Module();
            HIS.Desktop.ADO.TreatmentLogADO ado = new Desktop.ADO.TreatmentLogADO();
            HIS.Desktop.Common.RefeshReference refresh = null;
            HIS.Desktop.ADO.ExamTreatmentFinishADO exampAdo = new Desktop.ADO.ExamTreatmentFinishADO();
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is HIS.Desktop.ADO.TreatmentLogADO)
                            ado = (HIS.Desktop.ADO.TreatmentLogADO)item;
                        if (item is HIS.Desktop.Common.RefeshReference)
                            refresh = (HIS.Desktop.Common.RefeshReference)item;
                        if (item is HIS.Desktop.ADO.ExamTreatmentFinishADO)
                        {
                            exampAdo = (HIS.Desktop.ADO.ExamTreatmentFinishADO)item;
                        }
                    }
                }
                if (moduleData != null && ado!= null && ado.TreatmentId != 0)
                {
                    return new FormTreatmentFinish(ado, refresh, moduleData);
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
