using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrImpMestPrintFilter.AggrImpMestPrintFilter
{
    class AggrImpMestPrintFilterFactory
    {
        internal static IAggrImpMestPrintFilter MakeIAggrImpMestPrintFilter(CommonParam param, object[] data)
        {
            IAggrImpMestPrintFilter result = null;
            try
            {
                result = new AggrImpMestPrintFilterBehavior(param, data);
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
