using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.TreatmentLog;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventec.Desktop.Plugins.TreatmentLog.TreatmentLog
{
    class TreatmentLogFactory
    {
        internal static ITreatmentLog MakeITreatmentLog(CommonParam param, object[] data)
        {
            ITreatmentLog result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            TreatmentLogADO TreatmentADO = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is TreatmentLogADO)
                            {
                                TreatmentADO = (TreatmentLogADO)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                             moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }                           
                        }

                        if (moduleData != null && TreatmentADO != null)
                        {
                            result = new TreatmentLogBehavior(param, moduleData, (TreatmentLogADO)TreatmentADO);
                        }
                        
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
