using HIS.Desktop.Plugins.HisTreatmentRecordChecking.RecordChecking;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.HisTreatmentRecordChecking.HisTreatmentRecordChecking
{
    class HisTreatmentRecordCheckingBehavior : Tool<IDesktopToolContext>, IHisTreatmentRecordChecking
    {
        object[] entity;
        internal HisTreatmentRecordCheckingBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisTreatmentRecordChecking.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long? treatmentId = null;
                List<long> listTreatmentId = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is long)
                        {
                            treatmentId = (long)entity[i];
                        }
                        else if (entity[i] is List<long>)
                        {
                            listTreatmentId = (List<long>)entity[i];
                        }
                    }
                }
                if (listTreatmentId != null)
                {
                    return new FormHisTreatmentRecordChecking(moduleData, treatmentId, listTreatmentId);
                }
                else
                {
                    return new FormHisTreatmentRecordChecking(moduleData, treatmentId);
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
