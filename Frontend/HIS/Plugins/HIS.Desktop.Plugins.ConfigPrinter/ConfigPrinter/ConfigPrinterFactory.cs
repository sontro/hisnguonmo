using Inventec.Core;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfigPrinter.ConfigPrinter
{
    class ConfigPrinterFactory
    {
        internal static IConfigPrinter MakeIConfigPrinter(CommonParam param, object[] data)
        {
            IConfigPrinter result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            List<SAR_PRINT_TYPE> printTypes = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is List<SAR_PRINT_TYPE>)
                            {
                                printTypes = (List<SAR_PRINT_TYPE>)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        if (moduleData != null && printTypes != null)
                        {
                            result = new ConfigPrinterBehavior(moduleData, param, printTypes);
                        }
                        else if (moduleData != null)
                        {
                            result = new ConfigPrinterBehavior(moduleData, param);
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
