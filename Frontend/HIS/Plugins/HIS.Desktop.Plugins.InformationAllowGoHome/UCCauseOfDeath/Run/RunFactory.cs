using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.Run
{
    class RunFactory
    {
        internal static IRun MakeIRun(CommonParam param, object data)
        {
            IRun result = null;
            try
            {
                if (data is CauseOfDeathADO)
                {
                    result = new RunBehavior(param,(CauseOfDeathADO)data);
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
