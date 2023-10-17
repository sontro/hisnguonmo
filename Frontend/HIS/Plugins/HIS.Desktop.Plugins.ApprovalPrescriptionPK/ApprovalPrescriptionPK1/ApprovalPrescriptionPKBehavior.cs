using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK.ApprovalPrescriptionPK
{
    class ApprovalPrescriptionPKBehavior : Tool<IDesktopToolContext>, IApprovalPrescriptionPK
    {
        object[] entity;

        internal ApprovalPrescriptionPKBehavior()
            : base()
        {

        }

        internal ApprovalPrescriptionPKBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IApprovalPrescriptionPK.Run()
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
                    result = new UCApprovalPrescriptionPK(module);
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
