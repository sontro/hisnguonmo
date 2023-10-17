using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MealRationDetail.MealRationDetail;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.MealRationDetail.MealRationDetail
{
    class MealRationDetailBehavior : BusinessBase, IMealRationDetail
    {
        object[] entity;
        internal MealRationDetailBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }

        object IMealRationDetail.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_RATION_SUM expMest = null;
                DelegateSelectData delegateSelectData = null;
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
                            if (entity[i] is MOS.EFMODEL.DataModels.V_HIS_RATION_SUM)
                            {
                                expMest = ((MOS.EFMODEL.DataModels.V_HIS_RATION_SUM)entity[i]);
                            }
                            if (entity[i] is DelegateSelectData)
                            {
                                delegateSelectData = (DelegateSelectData)entity[i];
                            }
                        }
                    }
                }

                return new frmMealRationDetail(moduleData,expMest, delegateSelectData);
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
