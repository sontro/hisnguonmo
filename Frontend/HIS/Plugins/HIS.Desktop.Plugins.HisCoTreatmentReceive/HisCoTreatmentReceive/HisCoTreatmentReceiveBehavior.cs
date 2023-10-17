using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisCoTreatmentReceive.HisCoTreatmentReceive;

namespace HIS.Desktop.Plugins.HisCoTreatmentReceive.HisCoTreatmentReceive
{
    class HisCoTreatmentReceiveBehavior : BusinessBase, IHisCoTreatmentReceive
    {
        object[] entity;
        internal HisCoTreatmentReceiveBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisCoTreatmentReceive.Run()
        {
            try
            {
                long CotreatmentId = 0;
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                DelegateSelectData _delegateSelect = null;
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is DelegateSelectData)
                            {
                                _delegateSelect = (DelegateSelectData)entity[i];
                            }
                            if (entity[i] is long)
                            {
                                CotreatmentId = (long)entity[i];
                            }
                        }
                    }
                }

                return new frmHisCoTreatmentReceive(CotreatmentId, moduleData, _delegateSelect);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
