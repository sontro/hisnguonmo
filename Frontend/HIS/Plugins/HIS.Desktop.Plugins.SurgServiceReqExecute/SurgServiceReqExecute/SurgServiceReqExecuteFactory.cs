using HIS.Desktop.ADO;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.SurgServiceReqExecute.SurgServiceReqExecute
{
    class SurgServiceReqExecuteFactory
    {
        internal static ISurgServiceReqExecute MakeISurgServiceReqExecute(CommonParam param, object[] data)
        {
            ISurgServiceReqExecute result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_SERVICE_REQ serviceReq = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is V_HIS_SERVICE_REQ)
                            {
                                serviceReq = (V_HIS_SERVICE_REQ)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        result = new SurgServiceReqExecuteBehavior(param, serviceReq, moduleData);
                    }
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
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
