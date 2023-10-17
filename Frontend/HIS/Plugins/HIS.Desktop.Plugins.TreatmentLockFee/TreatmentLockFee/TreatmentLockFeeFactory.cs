using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentLockFee.TreatmentLockFee
{
    class TreatmentLockFeeFactory
    {
        internal static ITreatmentLockFee MakeITreatmentLockFee(CommonParam param, object[] data)
        {
            ITreatmentLockFee result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            long? treatmentId = null;
            try
            {

                if (data != null && data.Count() > 0)
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        if (data[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                        }
                        else if (data[i] is long)
                        {
                            treatmentId = (long)data[i];
                        }
                    }

                    if (moduleData != null && treatmentId.HasValue)
                    {
                        result = new TreatmentLockFeeBehavior(param, moduleData, treatmentId.Value);
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
