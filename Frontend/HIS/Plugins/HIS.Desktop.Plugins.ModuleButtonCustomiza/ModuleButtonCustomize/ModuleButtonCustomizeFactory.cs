using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ModuleButtonCustomize.ModuleButtonCustomize
{
    class ModuleButtonCustomizeFactory
    {
        internal static IModuleButtonCustomize MakeIModuleButtonCustomize(CommonParam param, object[] datas)
        {
            IModuleButtonCustomize result = null;
            try
            {
                result = new ModuleButtonCustomizeBehavior(param, datas);

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + datas.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => datas), datas), ex);
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
