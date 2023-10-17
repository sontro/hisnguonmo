using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodTransfusion.BloodTransfusion
{
    class BloodTransfusionFactory
    {
        internal static IBloodTransfusion MakeIBloodTransfusion(CommonParam param, object[] data)
        {
            IBloodTransfusion result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            string treatmentCode = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is string)
                            {
                                treatmentCode = (string)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        if (moduleData != null && !string.IsNullOrEmpty(treatmentCode))
                        {
                            result = new BloodTransfusionBehavior(moduleData, param, treatmentCode);
                        }
                        else
                        {
                            result = new BloodTransfusionBehavior(moduleData);
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
