using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalExportPrescription.ApprovalExportPrescription
{
    class ApprovalExportPrescriptionBehavior : Tool<IDesktopToolContext>, IApprovalExportPrescription
    {
        object[] entity;

        internal ApprovalExportPrescriptionBehavior()
            : base()
        {

        }

        internal ApprovalExportPrescriptionBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IApprovalExportPrescription.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                }
                if (module != null)
                {
                    result = new FormApprovalExportPrescription(module);
                }
                else
                    result = null;

                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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