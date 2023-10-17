using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.HisAssignBlood.ADO;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.HisAssignBlood.HisAssignBlood
{
    class HisAssignBloodFactory
    {
        internal static IHisAssignBlood MakeIHisAssignBlood(CommonParam param, object[] data)
        {
            IHisAssignBlood result = null;
            try
            {
                result = new HisAssignBloodBehavior(param, data);
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
