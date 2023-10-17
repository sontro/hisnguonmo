using HIS.Desktop.Plugins.Bordereau.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.Bordereau.Bordereau
{
    class BordereauFactory
    {
        internal static IBordereau MakeIBordereau(CommonParam param, object[] data)
        {
            IBordereau result = null;
            long treatmentId = 0;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
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
                                treatmentId = (long)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }                           
                        }

                        if (moduleData != null && treatmentId > 0)
                        {
                            result = new BordereauBehavior(param, treatmentId, moduleData);
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
