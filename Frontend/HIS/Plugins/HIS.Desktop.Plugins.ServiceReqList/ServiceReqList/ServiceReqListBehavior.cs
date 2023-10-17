using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.ServiceReqList
{
    class ServiceReqListBehavior : Tool<IDesktopToolContext>, IServiceReqList
    {
        object[] entity;

        internal ServiceReqListBehavior()
            : base()
        {

        }

        internal ServiceReqListBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IServiceReqList.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                MOS.EFMODEL.DataModels.HIS_TREATMENT treatment = null;
                MOS.EFMODEL.DataModels.V_HIS_PATIENT patient = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is MOS.EFMODEL.DataModels.HIS_TREATMENT)
                        {
                            treatment = (MOS.EFMODEL.DataModels.HIS_TREATMENT)entity[i];
                        }
                        else if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is MOS.EFMODEL.DataModels.V_HIS_PATIENT)
                        {
                            patient = (MOS.EFMODEL.DataModels.V_HIS_PATIENT)entity[i];
                        }
                    }
                }
                if (module != null)
                {
                    if (treatment != null)
                    {
                        result = new frmServiceReqList(module, treatment);
                    }
                    else if (patient != null)
                    {
                        result = new frmServiceReqList(module, patient);
                    }
                    else
                        result = new frmServiceReqList(module);
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
