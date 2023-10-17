using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;

namespace EMR.Desktop.Plugins.EmrConfig.EmrConfig
{
    class EmrConfigFactory
    {
        internal static IEmrConfig MakeIEmrConfig(CommonParam param, object[] data)
        {
            IEmrConfig Resutl = null;
            try
            {
                Resutl = new EmrConfigBehavior(param, data);
                if (Resutl == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                Resutl = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Resutl = null;
            }
            return Resutl;
        }
    }
}
