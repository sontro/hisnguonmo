using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AggrExpMestDetail.AggrExpMestDetail;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.AggrExpMestDetail.AggrExpMestDetail
{
    class ApproveAggrExpMestBehavior : BusinessBase, IApproveAggrExpMest
    {
        object[] entity;
        internal ApproveAggrExpMestBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }

        object IApproveAggrExpMest.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_EXP_MEST expMest = null;
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
                            if (entity[i] is MOS.EFMODEL.DataModels.V_HIS_EXP_MEST)
                            {
                                expMest = ((MOS.EFMODEL.DataModels.V_HIS_EXP_MEST)entity[i]);
                            }
                            if (entity[i] is DelegateSelectData)
                            {
                                delegateSelectData = (DelegateSelectData)entity[i];
                            }
                        }
                    }
                }

                return new frmAggrExpMestDetail(moduleData,expMest, delegateSelectData);
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
