using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.FeeHospitalWarning.FeeWarning
{
    class FeeWarningFactory
    {
        internal static IFeeWarning MakeIFeeWarning(CommonParam param, object[] data)
        {
            IFeeWarning result = null;
            try
            {
                //if (data.GetType() == typeof(HisDebateUserFilter))
                //{
                result = new FeeWarningBehavior(param, data);
                //}
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
