using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportBlood.ExportBlood
{
    class ExportBloodFactory
    {
        internal static IExportBlood MakeIExportBlood(CommonParam param, object[] data)
        {
            IExportBlood result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            long? expMestId = null;
            V_HIS_EXP_MEST expMest = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is long)
                            {
                                expMestId = (long)data[i];
                            }
                            else if (data[i] is V_HIS_EXP_MEST)
                            {
                                expMest = (V_HIS_EXP_MEST)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        if (moduleData != null && expMest != null)
                        {
                            result = new ExportBloodBehavior(moduleData, param, expMest);
                        }
                        else if (moduleData != null && expMestId.HasValue)
                        {
                            result = new ExportBloodBehavior(moduleData, param, expMestId.Value);
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
