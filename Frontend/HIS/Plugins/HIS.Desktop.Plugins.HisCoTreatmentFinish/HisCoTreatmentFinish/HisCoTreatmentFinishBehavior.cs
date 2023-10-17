using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisCoTreatmentFinish.HisCoTreatmentFinish;

namespace HIS.Desktop.Plugins.HisCoTreatmentFinish.HisCoTreatmentFinish
{
    class HisCoTreatmentFinishBehavior : BusinessBase, IHisCoTreatmentFinish
    {
        object[] entity;
        internal HisCoTreatmentFinishBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisCoTreatmentFinish.Run()
        {
            try
            {
                long coTreatmentId = 0;
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
                                coTreatmentId = (long)entity[i];
                            }
                        }
                    }
                }

                return new frmHisCoTreatmentFinish(coTreatmentId, moduleData, _delegateSelect);
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
