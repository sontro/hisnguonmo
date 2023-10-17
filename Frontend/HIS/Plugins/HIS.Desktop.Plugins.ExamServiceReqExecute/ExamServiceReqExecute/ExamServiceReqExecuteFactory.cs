using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.ExamServiceReqExecute.ExamServiceReqExecute
{
    class ExamServiceReqExecuteFactory
    {
        internal static IExamServiceReqExecute MakeIExamServiceReqExecute(CommonParam param, object[] data)
        {
            IExamServiceReqExecute result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_SERVICE_REQ serviceReq = null;
            List<HIS_SERE_SERV> sereServCurrentTreatment = null;
            DelegateSelectData reLoadServiceReq = null;
            bool isChronic = false;
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
                            else if (data[i] is List<HIS_SERE_SERV>)
                            {
                                sereServCurrentTreatment = (List<HIS_SERE_SERV>)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is bool)
                            {
                                isChronic = (bool)data[i];
                            }
                            else if (data[i] is DelegateSelectData)
                            {
                                reLoadServiceReq = data[i] as DelegateSelectData;
                            }
                        }

                        result = new ExamServiceReqExecuteBehavior(param, serviceReq, moduleData, isChronic, reLoadServiceReq, sereServCurrentTreatment);
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
