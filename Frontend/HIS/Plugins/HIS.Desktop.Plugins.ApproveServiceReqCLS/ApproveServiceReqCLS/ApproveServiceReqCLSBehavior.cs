using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveServiceReqCLS.ApproveServiceReqCLS
{
    class ApproveServiceReqCLSBehavior : BusinessBase, IApproveServiceReqCLS
    {
        object[] entity;
        internal ApproveServiceReqCLSBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IApproveServiceReqCLS.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_TREATMENT_FEE treatment = null;

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
                            if (entity[i] is V_HIS_TREATMENT_FEE)
                            {
                                treatment = (V_HIS_TREATMENT_FEE)entity[i];
                            }
                        }
                    }
                }

                return new frmApproveServiceReqCLS(moduleData, treatment);
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
