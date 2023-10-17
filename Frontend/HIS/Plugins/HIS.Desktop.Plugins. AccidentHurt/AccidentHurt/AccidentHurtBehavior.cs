using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.AccidentHurt.AccidentHurt
{
    class AccidentHurtBehavior : Tool<IDesktopToolContext>, IAccidentHurt
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal AccidentHurtBehavior()
            : base()
        {

        }

        internal AccidentHurtBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IAccidentHurt.Run()
        {
            object result = null;

            try
            {
                HIS.Desktop.Common.DelegateRefeshTreatmentPartialData dlg = null;
                if (entity != null && entity.Count() > 0)
                {
                    bool isTreatmentList = false;
                    foreach (var item in entity)
                    {
                        if (item is long)
                        {
                            treatmentId = (long)item;
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is bool)
                        {
                            isTreatmentList = (bool)item;
                        }
                        else if (item is HIS.Desktop.Common.DelegateRefeshTreatmentPartialData)
                        {
                            dlg = (HIS.Desktop.Common.DelegateRefeshTreatmentPartialData)item;
                        }
                    }

                    if (currentModule != null && treatmentId > 0)
                    {
                        result = new frmAccidentHurt(currentModule, treatmentId, isTreatmentList, dlg);
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
