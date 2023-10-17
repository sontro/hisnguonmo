
using HIS.Desktop.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarPrintList
{
    class SarPrintListFactory
    {
        internal static ISarPrintList MakeISarPrintList(CommonParam param, object[] data)
        {
            ISarPrintList result = null;
            SarPrintADO sarPrintADO = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is SarPrintADO)
                            {
                                sarPrintADO = (SarPrintADO)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }                           
                        }

                        if (moduleData != null && sarPrintADO!=null)
                        {
                            result = new SarPrintListBehavior(param, sarPrintADO, moduleData);
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
