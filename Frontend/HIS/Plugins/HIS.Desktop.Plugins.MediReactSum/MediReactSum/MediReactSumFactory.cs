using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediReactSum.MediReactSum
{
    class MediReactSumFactory
    {
        internal static IMediReactSum MakeIMediReactSum(CommonParam param, object[] data)
        {
            IMediReactSum result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            long? hisTreatmentId = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        bool isTreatmentList = false;
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is long)
                            {
                                hisTreatmentId = (long)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is bool)
                            {
                                isTreatmentList = (bool)data[i];
                            }
                        }

                        if (moduleData != null && hisTreatmentId.HasValue)
                        {
                            result = new MediReactSumBehavior(moduleData, param, hisTreatmentId.Value, isTreatmentList);
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
